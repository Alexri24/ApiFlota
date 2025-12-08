using Microsoft.AspNetCore.Mvc;
using ApiFlota.Services;
using ApiFlota.Models;

namespace ApiFlota.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class MantenimientoController : ControllerBase
   {
    private static List<Mantenimiento> mantenimientos = new List<Mantenimiento>();

    private readonly IMantenimientoService _conductorService;

    public MantenimientoController (IMantenimientoService conductorService)
        {
            _conductorService = conductorService;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Mantenimiento>>> GetMantenimiento()
        {
            var mantenimientos = await _conductorService.GetAllAsync();
            return Ok(mantenimientos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Mantenimiento>> GetMantenimiento(int id)
        {
            var mantenimientos = await _conductorService.GetByIdAsync(id);
            if (mantenimientos == null)
            {
                return NotFound();
            }
            return Ok(mantenimientos);
        }

        [HttpPost]
        public async Task<ActionResult<Mantenimiento>> CreateMantenimiento(Mantenimiento mantenimiento)
        {
            await _conductorService.AddAsync(mantenimiento);
            return CreatedAtAction(nameof(GetMantenimiento), new { id = mantenimiento.Id }, mantenimiento);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMantenimiento(int id, Mantenimiento updatedMantenimiento)
        {
            var existingMantenimiento = await _conductorService.GetByIdAsync(id);
            if (existingMantenimiento == null)
            {
                return NotFound();
            }

            // Actualizar el mantenimiento existente
            existingMantenimiento.Tipo = updatedMantenimiento.Tipo;
            existingMantenimiento.Coste = updatedMantenimiento.Coste;
            existingMantenimiento.HorasTrabajo = updatedMantenimiento.HorasTrabajo;
            existingMantenimiento.EsPreventivo= updatedMantenimiento.EsPreventivo;
            existingMantenimiento.FechaProgramada = updatedMantenimiento.FechaProgramada;
            existingMantenimiento.Decripcion = updatedMantenimiento.Decripcion;

            await _conductorService.UpdateAsync(existingMantenimiento);
            return NoContent();
        }
  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteMantenimiento(int id)
       {
           var mantenimiento = await _conductorService.GetByIdAsync(id);
           if (mantenimiento == null)
           {
               return NotFound();
           }
           await _conductorService.DeleteAsync(id);
           return NoContent();
       }

        [HttpPost("inicializar")]
        public async Task<IActionResult> InicializarDatos()
        {
            await _conductorService.InicializarDatosAsync();
            return Ok("Datos inicializados correctamente.");
        }

   }
}