using ApiFlota.Models;

    namespace ApiFlota.Repositories
{
    public interface IConductorRepository
        {
            Task<List<Conductor>> GetAllAsync();
            Task<Conductor?> GetByIdAsync(int id);
            Task AddAsync(Conductor conductor);
            Task UpdateAsync(Conductor conductor);
            Task DeleteAsync(int id);
            Task InicializarDatosAsync();
    }

}
    