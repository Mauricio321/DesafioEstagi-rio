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


        public IEnumerable<Filme> GetFilmes(int paginas, int quantidadeFilmesPorPagina, List<int> generoIds, string ator, OrdenacaoAvaliacao ordenacaoAvaliacao)
        {
            var paginasPassadas = paginas - 1;

            var paginasSkip = quantidadeFilmesPorPagina * paginasPassadas;

            var filmesFiltrados = context.Filmes.Where(l => l.FilmeId != null);

            var filmes = context.Filmes.ToList();

            //filtrar por genero
            if (generoIds != null)
            {
                filmesFiltrados = context.Filmes.Where(g => g.Generos == generoIds);
            }

            //ordem alfabetica 
            if (filmes != null)
            {
                filmesFiltrados.OrderBy(f => f.Nome);
            }

            //filtrar por ator
            if (ator != null)
            {
                filmesFiltrados = context.Filmes.Where(f => f.Atores == ator);
            }

            return filmesFiltrados;
        }

        public IEnumerable<Filme> GetFilmess()
        {
            var filmesFiltrados = context.Filmes.Where(l => l.FilmeId != null);
            return filmesFiltrados;
        }

        public IEnumerable<int> Avaliacoes()
        {
            IEnumerable<int> avaliacao = context.Avaliacoes.Select(a => a.Nota);

            return avaliacao;
        }

        public int QuantidadeAvaliacoes()
        {
            int quantidadeAvaliacoes = context.Avaliacoes.Select(a => a.Nota).Sum();

            return quantidadeAvaliacoes;
        }

        public void SaveChangesAsync()
        {
             context.SaveChanges();
        }

        public  Filme? FiltrarFilmePorId(int id)
        {
            var filme =  context.Filmes.FirstOrDefault(f => f.FilmeId == id);
            return filme;
        }
    }
}
