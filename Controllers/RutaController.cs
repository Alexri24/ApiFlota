using Microsoft.AspNetCore.Mvc;
using ApiFlota.Services;
using ApiFlota.Models;

namespace ApiFlota.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class RutaController : ControllerBase
   {
    private static List<Ruta> rutas = new List<Ruta>();

    private readonly IRutaService _rutaService;

    public RutaController (IRutaService rutaService)
        {
            _rutaService = rutaService;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Ruta>>> GetRuta()
        {
            var rutas = await _rutaService.GetAllAsync();
            return Ok(rutas);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Ruta>> GetRuta(int id)
        {
            var rutas = await _rutaService.GetByIdAsync(id);
            if (rutas == null)
            {
                return NotFound();
            }
            return Ok(rutas);
        }

        [HttpPost]
        public async Task<ActionResult<Ruta>> CreateRuta(Ruta ruta)
        {
            await _rutaService.AddAsync(ruta);
            return CreatedAtAction(nameof(GetRuta), new { id = ruta.Id }, ruta);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRuta(int id, Ruta updatedRuta)
        {
            var existingRuta = await _rutaService.GetByIdAsync(id);
            if (existingRuta == null)
            {
                return NotFound();
            }

            // Actualizar el ruta existente
            existingRuta.Origen = updatedRuta.Origen;
            existingRuta.Destino = updatedRuta.Destino;
            existingRuta.DistanciaKm = updatedRuta.DistanciaKm;
            existingRuta.Prioridad= updatedRuta.Prioridad;
            existingRuta.DuracionEstimada = updatedRuta.DuracionEstimada;
            existingRuta.FechaInicio = updatedRuta.FechaInicio;

            await _rutaService.UpdateAsync(existingRuta);
            return NoContent();
        }
  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteRuta(int id)
       {
           var ruta = await _rutaService.GetByIdAsync(id);
           if (ruta == null)
           {
               return NotFound();
           }
           await _rutaService.DeleteAsync(id);
           return NoContent();
       }

        [HttpPost("inicializar")]
        public async Task<IActionResult> InicializarDatos()
        {
            await _rutaService.InicializarDatosAsync();
            return Ok("Datos inicializados correctamente.");
        }

   }
}