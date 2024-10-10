using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Model;

namespace WebApi.Interface
{
    public interface IEstudianteService
    {
        Task<int?> RegistrarEstudiante(Estudiante estudiante);
        Task<bool> ActualizarEstudiante(Estudiante estudiante);
        Task<bool> EliminarEstudiante(int estuid);
        Task<Estudiante> ObtenerEstudiante(int estuid);
        Task<IEnumerable<Estudiante>> ListarEstudiante();
    }
}