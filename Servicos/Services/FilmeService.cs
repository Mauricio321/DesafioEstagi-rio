using Dominio.Models;
using FluentResults;
using Servicos.DTOs;
using Servicos.Interfaces;
using Servicos.RepositoryInterfaces;
using Servicos.Services.ServiceInterfaces;

namespace Servicos.Services
{
    public class FilmeService : IFilmeService
    {
        private readonly IFilmeRepository filmeRepository;
        private readonly IGeneroRepository generoRepository;

        public FilmeService(IFilmeRepository filmeRepository, IGeneroRepository generoRepository)
        {
            this.filmeRepository = filmeRepository;
            this.generoRepository = generoRepository;   
        }

        public string AdicionarFilmes(FilmeDTO filmeDTO)
        {
            var genero = generoRepository.GetGeneros(filmeDTO.GeneroId);

            var generoNaoEncontrado = filmeDTO.GeneroId.Where(generoId => !genero.Any(generos => generos.GeneroId == generoId));

            if (generoNaoEncontrado.Any())
            {
                return "genero nao encontrado";
            }

            var generos = genero.Select(genero =>
            new FilmeGenero
            {
                Genero = genero
            });

            var filmes = new Filme
            {
                Nome = filmeDTO.Nome,
                Duracao = filmeDTO.Duracao,
                FaixaEtaria = filmeDTO.FaixaEtaria,
                AnoDeLancamento = filmeDTO.AnoDeLancamento,
                Atores = filmeDTO.Atores,
                Direcao = filmeDTO.Direcao,
                Roteiristas = filmeDTO.Roteiristas,
                Generos = generos.ToList(),
            };

            filmeRepository.AdicionarFilmes(filmes);
            filmeRepository.SaveChanges();

            return "livro adicionado com sucesso";
        }

        public void DeleteFilme(Filme filme)
        {
            filmeRepository.DeleteFilme(filme);
        }

        public ListaDeFilmes GetFilmes(int paginas, int quantidadeFilmesPorPagina, List<int> generoIds, string ator, OrdenacaoAvaliacao ordenacaoAvaliacao)
        {
            IEnumerable<Filme> filmesFiltrados = filmeRepository.GetFilmess();

            var paginasPassadas = paginas - 1;

            var paginasSkip = quantidadeFilmesPorPagina * paginasPassadas;

            if (ordenacaoAvaliacao == OrdenacaoAvaliacao.MaiorParaMenor)
            {
                filmesFiltrados = filmeRepository.GetFilmess().OrderBy(f => f.NotaMedia);
            }

            if (ordenacaoAvaliacao == OrdenacaoAvaliacao.MenorParaMaior)
            {
                filmesFiltrados = filmeRepository.GetFilmess().OrderByDescending(f => f.NotaMedia);
            }

            var listaDeFilmes = new ListaDeFilmes
            {
                Filmes = filmesFiltrados.Skip(paginasSkip).Take(quantidadeFilmesPorPagina)
            };

            return listaDeFilmes;
        }
    }
}
