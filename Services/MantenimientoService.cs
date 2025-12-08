using ApiFlota.Models;
using ApiFlota.Repositories;

namespace ApiFlota.Services
{
public class MantenimientoService : IMantenimientoService
    {
        private readonly IMantenimientoRepository _mantenimientoRepository;

        public MantenimientoService(IMantenimientoRepository mantenimientoRepository)
        {
            _mantenimientoRepository= mantenimientoRepository;
            
        }

        public async Task<List<Mantenimiento>> GetAllAsync()
        {
            return await _mantenimientoRepository.GetAllAsync();
        }

        public async Task<Mantenimiento?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            return await _mantenimientoRepository.GetByIdAsync(id);
        }
//PONER TODOS LOS ELEMENTOS
        public async Task AddAsync(Mantenimiento mantenimiento)
        {
            if (string.IsNullOrWhiteSpace(mantenimiento.Descripcion))
                throw new ArgumentException("La matrícula del camión no puede estar vacía.");


            await _mantenimientoRepository.AddAsync(mantenimiento);
        }
//PONER TODOS LOS ELEMENTOS
        public async Task UpdateAsync(Mantenimiento mantenimiento)
        {
            if (mantenimiento.Id <= 0)
                throw new ArgumentException("El ID no es válido para actualización.");

            if (string.IsNullOrWhiteSpace(mantenimiento.Descripcion))
                throw new ArgumentException("La descripción puede estar vacía.");

            await _mantenimientoRepository.UpdateAsync(mantenimiento);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido para eliminación.");

            await _mantenimientoRepository.DeleteAsync(id);
        }

        public async Task InicializarDatosAsync() {
            await _mantenimientoRepository.InicializarDatosAsync();
        }
    }


}
    