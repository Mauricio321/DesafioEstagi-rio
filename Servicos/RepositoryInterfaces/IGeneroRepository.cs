using Dominio.Models;

namespace Servicos.RepositoryInterfaces
{
    public interface IGeneroRepository
    {
        Task AddGenero(Genero genero);
        Task<IEnumerable<Genero>> GetGeneros(List<int> id);
        void DeleteGenero(int id);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Genero>> GenerosDisponiveis();
    }
}
