using Microsoft.AspNetCore.Mvc;
using ApiFlota.Services;
using ApiFlota.Models;

namespace ApiFlota.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class ConductorController : ControllerBase
   {
    private static List<Conductor> conductores = new List<Conductor>();

    private readonly IConductorService _conductorService;

    public ConductorController (IConductorService conductorService)
        {
            _conductorService = conductorService;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Conductor>>> GetConductor()
        {
            var conductores = await _conductorService.GetAllAsync();
            return Ok(conductores);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Conductor>> GetConductor(int id)
        {
            var conductores = await _conductorService.GetByIdAsync(id);
            if (conductores == null)
            {
                return NotFound();
            }
            return Ok(conductores);
        }

        [HttpPost]
        public async Task<ActionResult<Conductor>> CreateConductor(Conductor conductor)
        {
            await _conductorService.AddAsync(conductor);
            return CreatedAtAction(nameof(GetConductor), new { id = conductor.Id }, conductor);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConductor(int id, Conductor updatedConductor)
        {
            var existingConductor = await _conductorService.GetByIdAsync(id);
            if (existingConductor == null)
            {
                return NotFound();
            }

            // Actualizar el conductor existente
            existingConductor.Nombre = updatedConductor.Nombre;
            existingConductor.Licencia = updatedConductor.Licencia;
            existingConductor.Antiguedad = updatedConductor.Antiguedad;
            existingConductor.SalarioBase= updatedConductor.SalarioBase;
            existingConductor.EsInternacional = updatedConductor.EsInternacional;
            existingConductor.FechaNacimiento = updatedConductor.FechaNacimiento;

            await _conductorService.UpdateAsync(existingConductor);
            return NoContent();
        }
  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteConductor(int id)
       {
           var conductor = await _conductorService.GetByIdAsync(id);
           if (conductor == null)
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