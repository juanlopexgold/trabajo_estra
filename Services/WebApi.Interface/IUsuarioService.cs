using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Interface
{
    public interface IUsuarioService
    {
        Task<int?> RegistrarUsuario(Usuarios usuario);
        Task<bool> ActualizarUsuario(Usuarios usuario);
        Task<Usuarios> ObtenerUsuario(int userId);
        Task<IEnumerable<Usuarios>> ListarUsuarios();
        Task<bool> AsignarRol(int userId, int roleId);
        Task<IEnumerable<string>> ObtenerRoles(int userId);
        Task<bool> CambiarContrasena(int userId, string passwordHash);
        Task<bool> DeshabilitarUsuario(int userId);
    }
}