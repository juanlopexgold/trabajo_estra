using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Interface;
using WebApi.Model;

namespace WebApi_CellShopCenter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            var token = await _authService.Authenticate(userLogin);
            if (token != null)
                return Ok(new { Token = token });
            return Unauthorized("Credenciales inválidas");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Usuario user)
        {
            await _authService.Register(user);
            return Ok("Usuario registrado exitosamente");
        }
    }
}