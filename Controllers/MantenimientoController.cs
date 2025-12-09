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

    private readonly IMantenimientoService _mantenimientoService;

    public MantenimientoController (IMantenimientoService mantenimientoService)
        {
            _mantenimientoService = mantenimientoService;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Mantenimiento>>> GetMantenimiento()
        {
            var mantenimientos = await _mantenimientoService.GetAllAsync();
            return Ok(mantenimientos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Mantenimiento>> GetMantenimiento(int id)
        {
            var mantenimientos = await _mantenimientoService.GetByIdAsync(id);
            if (mantenimientos == null)
            {
                return NotFound();
            }
            return Ok(mantenimientos);
        }

        [HttpPost]
        public async Task<ActionResult<Mantenimiento>> CreateMantenimiento(Mantenimiento mantenimiento)
        {
            await _mantenimientoService.AddAsync(mantenimiento);
            return CreatedAtAction(nameof(GetMantenimiento), new { id = mantenimiento.Id }, mantenimiento);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMantenimiento(int id, Mantenimiento updatedMantenimiento)
        {
            var existingMantenimiento = await _mantenimientoService.GetByIdAsync(id);
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
            existingMantenimiento.Descripcion = updatedMantenimiento.Descripcion;

            await _mantenimientoService.UpdateAsync(existingMantenimiento);
            return NoContent();
        }
  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteMantenimiento(int id)
       {
           var mantenimiento = await _mantenimientoService.GetByIdAsync(id);
           if (mantenimiento == null)
           {
               return NotFound();
           }
           await _mantenimientoService.DeleteAsync(id);
           return NoContent();
       }

        [HttpPost("inicializar")]
        public async Task<IActionResult> InicializarDatos()
        {
            await _mantenimientoService.InicializarDatosAsync();
            return Ok("Datos inicializados correctamente.");
        }

   }
}