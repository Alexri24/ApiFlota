using ApiFlota.Models;

    namespace ApiFlota.Repositories
{
    public interface IMantenimientoRepository
        {
            Task<List<Mantenimiento>> GetAllAsync();
            Task<Mantenimiento?> GetByIdAsync(int id);
            Task AddAsync(Mantenimiento mantenimiento);
            Task UpdateAsync(Mantenimiento mantenimiento);
            Task DeleteAsync(int id);
            Task InicializarDatosAsync();
    }

}
    