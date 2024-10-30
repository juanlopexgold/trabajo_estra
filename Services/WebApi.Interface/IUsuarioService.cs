using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Model;

namespace WebApi.Interface
{
    public interface IUsuarioService
    {
        Task<int?> RegistrarUsuario(Usuario usuario);
        Task<bool> ActualizarUsuario(Usuario usuario);
        Task<Usuario> ObtenerUsuario(int userId);
        Task<IEnumerable<Usuario>> ListarUsuarios();
        Task<bool> AsignarRol(int userId, int roleId);
        Task<IEnumerable<string>> ObtenerRoles(int userId);
        Task<bool> CambiarContrasena(int userId, string passwordHash);
        Task<bool> DeshabilitarUsuario(int userId);
    }
}