using Dominio.Models;

namespace Servicos.RepositoryInterfaces
{
    public interface IGeneroRepository
    {
        Task AddGenero(Genero genero);
        IEnumerable<Genero> GetGeneros(List<int> id);
        Task DeleteGenero(int id);
        void SaveChanges();
    }
}
