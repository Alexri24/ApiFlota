using ApiFlota.Models;

namespace ApiFlota.Services
{
    public interface ICamionService
    {
        Task<List<Camion>> GetAllAsync();
        Task<Camion?> GetByIdAsync(int id);
        Task AddAsync(Camion camion);
        Task UpdateAsync(Camion camion);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();

    }
}
