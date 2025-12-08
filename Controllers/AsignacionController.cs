using Microsoft.AspNetCore.Mvc;
using ApiFlota.Services;
using ApiFlota.Models;

namespace ApiFlota.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class AsignacionController : ControllerBase
   {
    private static List<Asignacion> asignaciones = new List<Asignacion>();

    private readonly IAsignacionService _asignacionService;

    public AsignacionController (IAsignacionService asignacionService)
        {
            _asignacionService = asignacionService;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Asignacion>>> GetAsignacion()
        {
            var asignaciones = await _asignacionService.GetAllAsync();
            return Ok(asignaciones);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Asignacion>> GetAsignacion(int id)
        {
            var asignaciones = await _asignacionService.GetByIdAsync(id);
            if (asignaciones == null)
            {
                return NotFound();
            }
            return Ok(asignaciones);
        }

        [HttpPost]
        public async Task<ActionResult<Asignacion>> CreateAsignacion(Asignacion asignacion)
        {
            await _asignacionService.AddAsync(asignacion);
            return CreatedAtAction(nameof(GetAsignacion), new { id = asignacion.Id }, asignacion);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsignacion(int id, Asignacion updatedAsignacion)
        {
            var existingAsignacion = await _asignacionService.GetByIdAsync(id);
            if (existingAsignacion == null)
            {
                return NotFound();
            }

            // Actualizar el asignacion existente
            existingAsignacion.CamionId = updatedAsignacion.CamionId;
            existingAsignacion.ConductorId= updatedAsignacion.ConductorId;
            existingAsignacion.FechaAsignacion = updatedAsignacion.FechaAsignacion;
            existingAsignacion.FechaFin= updatedAsignacion.FechaFin;
            existingAsignacion.EstaActiva = updatedAsignacion.EstaActiva;
            existingAsignacion.Motivo = updatedAsignacion.Motivo;
            existingAsignacion.PrimaAsignacion = updatedAsignacion.PrimaAsignacion;

            await _asignacionService.UpdateAsync(existingAsignacion);
            return NoContent();
        }
  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteAsignacion(int id)
       {
           var asignacion = await _asignacionService.GetByIdAsync(id);
           if (asignacion == null)
           {
               return NotFound();
           }
           await _asignacionService.DeleteAsync(id);
           return NoContent();
       }

        [HttpPost("inicializar")]
        public async Task<IActionResult> InicializarDatos()
        {
            await _asignacionService.InicializarDatosAsync();
            return Ok("Datos inicializados correctamente.");
        }

   }
}