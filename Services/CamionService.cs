using ApiFlota.Models;
using ApiFlota.Repositories;

namespace ApiFlota.Services
{
public class CamionService : ICamionService
    {
        private readonly ICamionRepository _camionRepository;

        public CamionService(ICamionRepository camionRepository)
        {
            _camionRepository = camionRepository;
            
        }

        public async Task<List<Camion>> GetAllAsync()
        {
            return await _camionRepository.GetAllAsync();
        }

        public async Task<Camion?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            return await _camionRepository.GetByIdAsync(id);
        }
//PONER TODOS LOS ELEMENTOS
        public async Task AddAsync(Camion camion)
        {
            if (string.IsNullOrWhiteSpace(camion.Matricula))
                throw new ArgumentException("La matrícula del camión no puede estar vacía.");

            if (string.IsNullOrWhiteSpace(camion.Matricula))
                throw new ArgumentException("La matrícula debe ser mayor que cero.");

            await _camionRepository.AddAsync(camion);
        }
//PONER TODOS LOS ELEMENTOS
        public async Task UpdateAsync(Camion camion)
        {
            if (camion.Id <= 0)
                throw new ArgumentException("El ID no es válido para actualización.");

            if (string.IsNullOrWhiteSpace(camion.Matricula))
                throw new ArgumentException("El nombre del plato no puede estar vacío.");

            await _camionRepository.UpdateAsync(camion);
        }
//PONER TODOS LOS ELEMENTOS
        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido para eliminación.");

            await _camionRepository.DeleteAsync(id);
        }

        public async Task InicializarDatosAsync() {
            await _camionRepository.InicializarDatosAsync();
        }
    }


}
    