using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebApi.Interface;
using WebApi.Model;

namespace WebApi.Implementation
{
    public class UsuarioService : IUsuarioService
    {
        private readonly string _connectionString;

        public UsuarioService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int?> RegistrarUsuario(Usuario usuario)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spRegistrarUsuario", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", usuario.Username);
                cmd.Parameters.AddWithValue("@PasswordHash", usuario.PasswordHash);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                return usuario.IdUsuario;
            }
        }

        public async Task<bool> ActualizarUsuario(Usuario usuario)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spActualizarUsuario", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                cmd.Parameters.AddWithValue("@Username", usuario.Username);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }

        public async Task<bool> DeshabilitarUsuario(int userId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeshabilitarUsuario", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", userId);
                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }

        public async Task<Usuario> ObtenerUsuario(int userId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spObtenerUsuario", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", userId);
                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Usuario
                        {
                            IdUsuario = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Email = reader.GetString(2),
                            FechaCreacion = reader.GetDateTime(3),
                            UltimoLogin = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4)
                        };
                    }
                }
                return null;
            }
        }

        public async Task<IEnumerable<Usuario>> ListarUsuarios()
        {
            var usuarios = new List<Usuario>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spListarUsuarios", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        usuarios.Add(new Usuario
                        {
                            IdUsuario = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Email = reader.GetString(2),
                            FechaCreacion = reader.GetDateTime(3),
                            UltimoLogin = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4)
                        });
                    }
                }
            }
            return usuarios;
        }

        public async Task<bool> AsignarRol(int userId, int roleId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAsignarRol", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", userId);
                cmd.Parameters.AddWithValue("@IdRol", roleId);
                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }

        public async Task<IEnumerable<string>> ObtenerRoles(int userId)
        {
            var roles = new List<string>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spObtenerRoles", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", userId);
                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        roles.Add(reader.GetString(0));
                    }
                }
            }
            return roles;
        }

        public async Task<bool> CambiarContrasena(int userId, string passwordHash)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spCambiarContrasena", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", userId);
                cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }
}