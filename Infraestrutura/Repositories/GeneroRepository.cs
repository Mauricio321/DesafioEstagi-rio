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
        public async Task AddGenero(Genero genero, CancellationToken cancellationToken)
        {
            await context.AddAsync(genero, cancellationToken);
        }

        public void DeleteGenero(Genero genero)
        {
            context.Generos.Remove(genero);
        }

        public async Task<IEnumerable<Genero>> GenerosDisponiveis()
        {
            var generos = await context.Generos.ToListAsync();
            return generos;
        }

        public async Task<Genero?> GetGenero(int id)
        {
            return await context.Generos.FindAsync(id);
        }

        public async Task<IEnumerable<Genero>> GetGeneros(List<int> id)
        {
            var genero = await context.Generos.Where(g => id.Contains(g.GeneroId)).ToListAsync();

            return genero;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            context.SaveChangesAsync();

            return Task.CompletedTask;
        }
    }
}
