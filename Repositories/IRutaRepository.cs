using ApiFlota.Models;

    namespace ApiFlota.Repositories
{
    public interface IRutaRepository
        {
            Task<List<R>> GetAllAsync();
            Task<Ruta?> GetByIdAsync(int id);
            Task AddAsync(Ruta ruta);
            Task UpdateAsync(Ruta ruta);
            Task DeleteAsync(int id);
            Task InicializarDatosAsync();
    }

}
    