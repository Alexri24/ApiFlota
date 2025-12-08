using ApiFlota.Models;

    namespace ApiFlota.Repositories
{
    public interface ICamionRepository
        {
            Task<List<Camion>> GetAllAsync();
            Task<Camion?> GetByIdAsync(int id);
            Task AddAsync(Camion camion);
            Task UpdateAsync(Camion camion);
            Task DeleteAsync(int id);
            Task InicializarDatosAsync();
    }

}
    