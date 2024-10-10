using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Interface;
using WebApi.Model;

namespace WebApi_Estudiantes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesController : ControllerBase
    {
        private readonly IEstudianteService _estudianteService;

        public EstudiantesController(IEstudianteService estudianteService)
        {
            _estudianteService = estudianteService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarEstudiante([FromBody] Estudiante estudiante)
        {
            var result = await _estudianteService.RegistrarEstudiante(estudiante);
            if (result.HasValue)
                return Ok(result.Value);
            return BadRequest("Error al registrar estudiante");
        }

        [HttpPut("actualizar")]
        public async Task<IActionResult> ActualizarEstudiante([FromBody] Estudiante estudiante)
        {
            var result = await _estudianteService.ActualizarEstudiante(estudiante);
            if (result)
                return Ok("estudiante actualizado exitosamente");
            return BadRequest("Error al actualizar estudiante");
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> EliminarEstudiante(int id)
        {
            var result = await _estudianteService.EliminarEstudiante(id);
            if (result)
                return Ok("estudiante eliminado exitosamente");
            return BadRequest("Error al eliminar estudiante");
        }

        [HttpGet("obtener/{id}")]
        public async Task<IActionResult> ObtenerEstudiante(int id)
        {
            var estudiante = await _estudianteService.ObtenerEstudiante(id);
            if (estudiante != null)
                return Ok(estudiante);
            return NotFound("estudiante no encontrado");
        }

        [HttpGet("listar")]
        public async Task<IActionResult> ListarEstudiante()
        {
            var estudiante = await _estudianteService.ListarEstudiante();
            return Ok(estudiante);
        }
    }
}