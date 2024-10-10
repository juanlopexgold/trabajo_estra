using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebApi.Interface;
using WebApi.Model;

namespace WebApi.Implementation
{
    public class EstudianteService : IEstudianteService
    {
        private readonly string _connectionString;

        public EstudianteService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int?> RegistrarEstudiante(Estudiante estudiante)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("InsertarRegistro", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre_estudiante", estudiante.NombreEstudiante);
                cmd.Parameters.AddWithValue("@fecha_llegada", estudiante.FechaLlegada);
                cmd.Parameters.AddWithValue("@hora_llegada", estudiante.HoraLlegada);
                cmd.Parameters.AddWithValue("@comentario", estudiante.Comentario);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                return estudiante.IdEstudiante;
            }
        }

        public async Task<bool> ActualizarEstudiante(Estudiante estudiante)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("EditarRegistro", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_estudiantes", estudiante.IdEstudiante);
                cmd.Parameters.AddWithValue("@nombre_estudiante", estudiante.NombreEstudiante);
                cmd.Parameters.AddWithValue("@fecha_llegada", estudiante.FechaLlegada);
                cmd.Parameters.AddWithValue("@hora_llegada", estudiante.HoraLlegada);
                cmd.Parameters.AddWithValue("@comentario", estudiante.Comentario);

                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }

        public async Task<bool> EliminarEstudiante(int estuid)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("EliminarRegistro", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_estudiantes", estuid);

                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }

        public async Task<Estudiante> ObtenerEstudiante(int estuid)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("MostrarRegistroPorID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_estudiantes", estuid);

                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Estudiante
                        {
                            IdEstudiante = reader.GetInt32(0),
                            NombreEstudiante = reader.GetString(1),
                            FechaLlegada = reader.GetDateTime(2),
                            HoraLlegada = reader.GetTimeSpan(3),
                            Comentario = reader.GetString(4)
                        };
                    }
                }
                return null;
            }
        }

        public async Task<IEnumerable<Estudiante>> ListarEstudiante()
        {
            var estudiantes = new List<Estudiante>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("MostrarUltimos50Registros", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        estudiantes.Add(new Estudiante
                        {
                            IdEstudiante = reader.GetInt32(0),
                            NombreEstudiante = reader.GetString(1),
                            FechaLlegada = reader.GetDateTime(2),
                            HoraLlegada = reader.GetTimeSpan(3),
                            Comentario = reader.GetString(4)
                        });
                    }
                }
            }
            return estudiantes;
        }
    }
}