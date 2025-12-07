using ApiFlota.Models;

namespace ApiFlota.Services
{
    public interface ICconductorService
    {
        Task<List<Conductor>> GetAllAsync();
        Task<Conductor?> GetByIdAsync(int id);
        Task AddAsync(Conductor conductor);
        Task UpdateAsync(Conductor conductor);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();

    }
}
