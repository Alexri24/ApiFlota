using Microsoft.AspNetCore.Mvc;
using ApiFlota.Services;
using ApiFlota.Models;

namespace ApiFlota.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class CamionController : ControllerBase
   {
    private static List<Camion> camiones = new List<Camion>();

    private readonly ICamionService _camionService;

    public CamionController (ICamionService camionService)
        {
            _camionService = camionService;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Camion>>> GetCamion()
        {
            var camiones = await _camionService.GetAllAsync();
            return Ok(camiones);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Camion>> GetCamion(int id)
        {
            var camiones = await _camionService.GetByIdAsync(id);
            if (camiones == null)
            {
                return NotFound();
            }
            return Ok(camiones);
        }

        [HttpPost]
        public async Task<ActionResult<Camion>> CreateCamion(Camion camion)
        {
            await _camionService.AddAsync(camion);
            return CreatedAtAction(nameof(GetCamion), new { id = camion.Id }, camion);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCamion(int id, Camion updatedCamion)
        {
            var existingCamion = await _camionService.GetByIdAsync(id);
            if (existingCamion == null)
            {
                return NotFound();
            }

            // Actualizar el camion existente
            existingCamion.Matricula = updatedCamion.Matricula;
            existingCamion.Marca = updatedCamion.Marca;
            existingCamion.Kilometraje = updatedCamion.Kilometraje;
            existingCamion.CapacidadCarga= updatedCamion.CapacidadCarga;
            existingCamion.EsOperativo = updatedCamion.EsOperativo;
            existingCamion.FechaCompra = updatedCamion.FechaCompra;

            await _camionService.UpdateAsync(existingCamion);
            return NoContent();
        }
  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteCamion(int id)
       {
           var camion = await _camionService.GetByIdAsync(id);
           if (camion == null)
           {
               return NotFound();
           }
           await _camionService.DeleteAsync(id);
           return NoContent();
       }

        [HttpPost("inicializar")]
        public async Task<IActionResult> InicializarDatos()
        {
            await _camionService.InicializarDatosAsync();
            return Ok("Datos inicializados correctamente.");
        }

   }
}