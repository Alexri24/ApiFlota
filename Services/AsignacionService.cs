using ApiFlota.Models;
using ApiFlota.Repositories;

namespace ApiFlota.Services
{
public class AsignacionService : IAsignacionService
    {
        private readonly IAsignacionRepository _asignacionRepository;

        public AsignacionServiceService(IAsignacionRepository asignacionRepository)
        {
            _asignacionRepository= asignacionRepository;
            
        }

        public async Task<List<Asignacion>> GetAllAsync()
        {
            return await _asignacionRepository.GetAllAsync();
        }

        public async Task<Asignacion?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            return await _asignacionRepository.GetByIdAsync(id);
        }
//PONER TODOS LOS ELEMENTOS
        public async Task AddAsync(Asignacion asignacion)
        {
            if ( Camion.Id <= 0)
                throw new ArgumentException("El ID del camión no ser menor que 0.");

            if ( Conductor.Id <= 0)
                throw new ArgumentException("El ID del conductor no ser menor que 0.");

            if (string.IsNullOrWhiteSpace(asignacion.FechaAsignacion))
                throw new ArgumentException("La Fecha no puede estar vacía.");

            await _asignacionRepository.AddAsync(asignacion);
        }
//PONER TODOS LOS ELEMENTOS
        public async Task UpdateAsync(Asignacion asignacion)
        {
            if (asignacion.Id <= 0)
                throw new ArgumentException("El ID no es válido para actualización.");

              if ( Camion.Id <= 0)
                throw new ArgumentException("El ID del camión no ser menor que 0.");

            if ( Conductor.Id <= 0)
                throw new ArgumentException("El ID del conductor no ser menor que 0.");

            if (string.IsNullOrWhiteSpace(asignacion.FechaAsignacion))
                throw new ArgumentException("La Fecha no puede estar vacía.");

            await _asignacionRepository.UpdateAsync(asignacion);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido para eliminación.");

            await _asignacionRepository.DeleteAsync(id);
        }

        public async Task InicializarDatosAsync() {
            await _asignacionRepository.InicializarDatosAsync();
        }
    }


}
    