using ApiFlota.Models;

namespace ApiFlota.Services
{
    public interface IMantenimientoService
    {
        Task<List<Mantenimiento>> GetAllAsync();
        Task<Mantenimiento?> GetByIdAsync(int id);
        Task AddAsync(Mantenimiento mantenimiento);
        Task UpdateAsync(Mantenimiento mantenimiento);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();

    }
}
