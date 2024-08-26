using Dominio.Models;
using Infraestrutura.Data;
using Microsoft.EntityFrameworkCore;
using Servicos.Interfaces;

namespace Infraestrutura.Repositorys
{
    public class FilmeRepository : IFilmeRepository
    {
        private readonly FilmeContext context;
        public FilmeRepository(FilmeContext context)
        {
            this.context = context;
        }

        public async void AdicionarFilmes(Filme filme)
        {
            await context.Filmes.AddAsync(filme);
        }

        public void DeleteFilme(Filme filme)
        {
            context.Filmes.Remove(filme);
        }


        public async Task<IEnumerable<Filme>> GetFilmes(int paginas, int quantidadeFilmesPorPagina, List<int> generoIds, string? ator, OrdenacaoAvaliacao ordenacaoAvaliacao)
        {
            var paginasPassadas = paginas - 1;

            var paginasSkip = quantidadeFilmesPorPagina * paginasPassadas;

            var filmes = context.Filmes.Include(f => f.Avaliacoes).AsQueryable();

            if (generoIds != null)
            {
                filmes = filmes.Where(g => g.Generos == generoIds);
            }

            if (ator != null)
            {
                filmes = filmes.Where(f => f.Atores == ator);
            }

            if (ordenacaoAvaliacao == OrdenacaoAvaliacao.MaiorParaMenor)
            {
                filmes = filmes.OrderBy(f => f.NotaMedia);
            }

            if (ordenacaoAvaliacao == OrdenacaoAvaliacao.MenorParaMaior)
            {
                filmes = filmes.OrderByDescending(f => f.NotaMedia);
            }

            return await filmes.ToListAsync();
        }

        public async Task<IEnumerable<int>> Avaliacoes()
        {
            IEnumerable<int> avaliacao = await context.Avaliacoes.Select(a => a.Nota).ToArrayAsync();

            return avaliacao;
        }

        public async Task<int> QuantidadeAvaliacoes()
        {
            int quantidadeAvaliacoes = await context.Avaliacoes.Select(a => a.Nota).SumAsync();

            return quantidadeAvaliacoes;
        }

        public Task<Filme?> FiltrarFilmePorId(int id)
        {
            var filme = context.Filmes.FirstOrDefaultAsync(f => f.FilmeId == id);
            return filme;
        }

        // Verificar depois
        public void AvaliacaoFilme(Avaliacao avaliacao)
        {
            context.Avaliacoes.AddAsync(avaliacao);
        }

        public Task<Filme?> FiltrarFilmePorIdAsync(int id)
        {
            return context.Filmes.FirstOrDefaultAsync(f => f.FilmeId == id);
        }

        public Task SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Usuario>> GetUsuario(int id)
        {
            return await context.Usuarios.Where(u => u.Id == id).ToListAsync();
        }

        public Task<Avaliacao?> UsuarioJaAvaliou(int usuarioId, int filmeId)
        {
            return context.Avaliacoes.FirstOrDefaultAsync(u => u.FilmeId == filmeId && u.UsuarioId == usuarioId);
        }

        public void AtualizarAvaliacao(Avaliacao avaliacao)
        {
            context.Avaliacoes.Update(avaliacao);

            context.SaveChanges();
        }
    }
}
