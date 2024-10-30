using System;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApi.Interface;
using WebApi.Model;
using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace WebApi.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly string _connectionString;

        public AuthService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<string> Authenticate(UserLogin userLogin)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT PasswordHash FROM Usuarios WHERE Username = @Username", conn);
                    cmd.Parameters.AddWithValue("@Username", userLogin.Username);
                    await conn.OpenAsync();
                    var storedHash = (string)await cmd.ExecuteScalarAsync();

                    if (storedHash != null && BCrypt.Net.BCrypt.Verify(userLogin.Password, storedHash))
                    {
                        SqlCommand loginCmd = new SqlCommand("spLoginUsuario", conn);
                        loginCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        loginCmd.Parameters.AddWithValue("@Username", userLogin.Username);
                        loginCmd.Parameters.AddWithValue("@PasswordHash", storedHash);
                        var result = await loginCmd.ExecuteScalarAsync();

                        if (result != DBNull.Value)
                        {
                            int userId = (int)result;
                            string token = GenerateJwtToken(userId);
                            await GenerarToken(userId, token, DateTime.UtcNow.AddHours(1));
                            return token;
                        }
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during authentication: {ex.Message}");
                throw;
            }
        }

        public async Task Register(Usuario user)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spRegistrarUsuario", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@PasswordHash", BCrypt.Net.BCrypt.HashPassword(user.PasswordHash));
                cmd.Parameters.AddWithValue("@Email", user.Email);
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        private string GenerateJwtToken(int userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("My$3cureJWTKey123!"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "CellShopCenter",
                audience: "Empleados",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task GenerarToken(int userId, string token, DateTime fechaExpiracion)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGenerarToken", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@Token", token);
                cmd.Parameters.AddWithValue("@FechaExpiracion", fechaExpiracion);
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<bool> VerificarToken(string token)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spVerificarToken", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Token", token);
                await conn.OpenAsync();
                var result = await cmd.ExecuteScalarAsync();
                return result != DBNull.Value;
            }
        }
    }
}