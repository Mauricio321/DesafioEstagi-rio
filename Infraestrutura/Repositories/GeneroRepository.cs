using Dominio.Models;
using Infraestrutura.Data;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Genero>> GenerosDisponiveis()
        {
            var generos = await context.Generos.ToListAsync();
            return generos;
        }

        public async Task<IEnumerable<Genero>> GetGeneros(List<int> id)
        {
            var genero = await context.Generos.Where(g => id.Contains(g.GeneroId)).ToListAsync();

            return genero;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
