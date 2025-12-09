using ApiFlota.Models;

namespace ApiFlota.Services
{
    public interface IAsignacionService
    {
        Task<List<Asignacion>> GetAllAsync();
        Task<Asignacion?> GetByIdAsync(int id);
        Task AddAsync(Asignacion asignacion);
        Task UpdateAsync(Asignacion asignacion);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();

    }
}
