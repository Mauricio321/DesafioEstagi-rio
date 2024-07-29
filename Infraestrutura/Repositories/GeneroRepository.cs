using Dominio.Models;
using Infraestrutura.Data;
using Servicos.RepositoryInterfaces;

namespace Infraestrutura.Repositories
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly FilmeContext context;
        public GeneroRepository(FilmeContext context)
        {
            this.context = context;
        }
        public async Task AddGenero(Genero genero)
        {
            await context.AddAsync(genero);
        }

        public void DeleteGenero(int id)
        {
            var generoId = context.Generos.Find(id);

            context.Generos.Remove(generoId);
        }

        public IEnumerable<Genero> GenerosDisponiveis()
        {
            var generos =  context.Generos.ToList();
            return generos;
        }

        public IEnumerable<Genero> GetGeneros(List<int> id)
        {
            var genero = context.Generos.Where(g => id.Contains(g.GeneroId));

            return genero;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
