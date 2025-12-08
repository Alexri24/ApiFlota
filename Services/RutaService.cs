using ApiFlota.Models;
using ApiFlota.Repositories;

namespace ApiFlota.Services
{
public class RutaService : IRutaService
    {
        private readonly IRutaRepository _rutaRepository;

        public RutaService(IRutaRepository rutaRepository)
        {
            _rutaRepository= rutaRepository;
            
        }

        public async Task<List<Ruta>> GetAllAsync()
        {
            return await _rutaRepository.GetAllAsync();
        }

        public async Task<Ruta?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            return await _rutaRepository.GetByIdAsync(id);
        }
//PONER TODOS LOS ELEMENTOS
        public async Task AddAsync(Ruta ruta)
        {
            if (string.IsNullOrWhiteSpace(ruta.Origen))
                throw new ArgumentException("El origen de la ruta no puede estar vacía.");

            if (string.IsNullOrWhiteSpace(ruta.Destino))
                throw new ArgumentException("El Destino de la ruta no puede estar vacío.");

            await _rutaRepository.AddAsync(ruta);
        }
//PONER TODOS LOS ELEMENTOS
        public async Task UpdateAsync(Ruta ruta)
        {
            if (ruta.Id <= 0)
                throw new ArgumentException("El ID no es válido para actualización.");

            if (string.IsNullOrWhiteSpace(ruta.Origen))
                throw new ArgumentException("El Origen de la ruta no puede estar vacío.");


            if (string.IsNullOrWhiteSpace(ruta.Destino))
                throw new ArgumentException("El Destino de la ruta no puede estar vacío.");

            await _rutaRepository.UpdateAsync(ruta);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido para eliminación.");

            await _rutaRepository.DeleteAsync(id);
        }

        public async Task InicializarDatosAsync() {
            await _rutaRepository.InicializarDatosAsync();
        }
    }


}
    