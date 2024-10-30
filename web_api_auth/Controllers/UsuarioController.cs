using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Interface;
using WebApi.Model;

namespace WebApi_CellShopCenter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("registrar")]
        // [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] Usuario usuario)
        {
            var result = await _usuarioService.RegistrarUsuario(usuario);
            if (result.HasValue)
                return Ok(result.Value);
            return BadRequest("Error al registrar usuario");
        }

        [HttpPut("actualizar")]
        // [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ActualizarUsuario([FromBody] Usuario usuario)
        {
            var result = await _usuarioService.ActualizarUsuario(usuario);
            if (result)
                return Ok("Usuario actualizado exitosamente");
            return BadRequest("Error al actualizar usuario");
        }

        [HttpDelete("eliminar/{id}")]
        // [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeshabilitarUsuario(int id)
        {
            var result = await _usuarioService.DeshabilitarUsuario(id);
            if (result)
                return Ok("Usuario eliminado exitosamente");
            return BadRequest("Error al eliminar usuario");
        }

        [HttpGet("obtener/{id}")]
        // [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ObtenerUsuario(int id)
        {
            var usuario = await _usuarioService.ObtenerUsuario(id);
            if (usuario != null)
                return Ok(usuario);
            return NotFound("Usuario no encontrado");
        }

        [HttpGet("listar")]
        // [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ListarUsuarios()
        {
            var usuarios = await _usuarioService.ListarUsuarios();
            return Ok(usuarios);
        }

        [HttpPost("asignar-rol")]
        // [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AsignarRol(int userId, int roleId)
        {
            var result = await _usuarioService.AsignarRol(userId, roleId);
            if (result)
                return Ok("Rol asignado exitosamente");
            return BadRequest("Error al asignar rol");
        }

        [HttpGet("obtener-roles/{id}")]
        // [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ObtenerRoles(int id)
        {
            var roles = await _usuarioService.ObtenerRoles(id);
            return Ok(roles);
        }

        [HttpPut("cambiar-contrasena")]
        // [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> CambiarContrasena(int userId, string passwordHash)
        {
            var result = await _usuarioService.CambiarContrasena(userId, passwordHash);
            if (result)
                return Ok("Contraseña cambiada exitosamente");
            return BadRequest("Error al cambiar contraseña");
        }
    }
}