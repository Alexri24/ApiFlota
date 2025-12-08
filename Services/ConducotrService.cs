using ApiFlota.Models;
using ApiFlota.Repositories;

namespace ApiFlota.Services
{
public class ConductorService : IConductorService
    {
        private readonly IConductorRepository _conductorRepository;

        public ConductorService(IConductorRepository conductorRepository)
        {
            _conductorRepository= conductorRepository;
            
        }

        public async Task<List<Conductor>> GetAllAsync()
        {
            return await _conductorRepository.GetAllAsync();
        }

        public async Task<Conductor?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            return await _conductorRepository.GetByIdAsync(id);
        }
//PONER TODOS LOS ELEMENTOS
        public async Task AddAsync(Conductor conductor)
        {
            if (string.IsNullOrWhiteSpace(conductor.Matricula))
                throw new ArgumentException("La matrícula del camión no puede estar vacía.");

            if (string.IsNullOrWhiteSpace(conductor.Matricula))
                throw new ArgumentException("La matrícula debe ser mayor que cero.");

            await _conductorRepository.AddAsync(conductor);
        }
//PONER TODOS LOS ELEMENTOS
        public async Task UpdateAsync(Conductor conductor)
        {
            if (conductor.Id <= 0)
                throw new ArgumentException("El ID no es válido para actualización.");

            if (string.IsNullOrWhiteSpace(conductor.Matricula))
                throw new ArgumentException("El nombre del plato no puede estar vacío.");

            await _conductorRepository.UpdateAsync(conductor);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido para eliminación.");

            await _conductorRepository.DeleteAsync(id);
        }

        public async Task InicializarDatosAsync() {
            await _conductorRepository.InicializarDatosAsync();
        }
    }


}
    