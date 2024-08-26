using Dominio.Models;

namespace Servicos.RepositoryInterfaces
{
    public interface IGeneroRepository
    {
        Task AddGenero(Genero genero, CancellationToken cancellationToken);
        Task<IEnumerable<Genero>> GetGeneros(List<int> id);
        void DeleteGenero(Genero genero);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Genero>> GenerosDisponiveis();
        Task<Genero?> GetGenero(int id);
    }
}
