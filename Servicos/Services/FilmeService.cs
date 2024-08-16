using Dominio.Models;
using FluentResults;
using Servicos.DTOs;
using Servicos.Interfaces;
using Servicos.RepositoryInterfaces;
using Servicos.Services.ServiceInterfaces;
using System.Diagnostics.CodeAnalysis;

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

        public async Task<Result> AdicionarFilmes(FilmeDTO filmeDTO)
        {
            var genero = await generoRepository.GetGeneros(filmeDTO.GeneroId);

            var generoNaoEncontrado = filmeDTO.GeneroId.Where(generoId => !genero.Any(generos => generos.GeneroId == generoId));

            if (generoNaoEncontrado.Any())
            {
                return Result.Fail("Genero não encontrado");
            }

            var generos =  genero.Select(genero =>
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

            return Result.Ok();
        }

        public async Task<Avaliacao> AvaliacaoFilme(AvaliacaoDTO avaliacaoDto, int usuarioId)
        {
            var avaliacao = await filmeRepository.UsuarioJaAvaliou(avaliacaoDto.FilmeId, usuarioId);

            if (avaliacao is not null)
            {
                avaliacao.Nota = avaliacaoDto.Nota;

                filmeRepository.AtualizarAvaliacao(avaliacao);
                filmeRepository.SaveChanges();

                return avaliacao;
            }

            var avaliacoes = new Avaliacao
            {
                FilmeId = avaliacaoDto.FilmeId,
                Nota = avaliacaoDto.Nota,
                Comentario = avaliacaoDto.Comentario,
                UsuarioId = usuarioId
            };

            filmeRepository.AvaliacaoFilme(avaliacoes);
            filmeRepository.SaveChanges();

            return avaliacoes;
        }

        public async Task DeleteFilmeAsync(int id)
        {
            var filme = await filmeRepository.FiltrarFilmePorIdAsync(id);

            filmeRepository.DeleteFilme(filme);

            await filmeRepository.SaveChangesAsync();
        }

        public async Task<List<Avaliacao>> GetAvaliacoes(int id)
        {
            var avaliacao = await filmeRepository.FiltrarFilmePorId(id);

            return avaliacao.Avaliacoes.ToList();
        }

        public async Task<ListaDeFilmesDto> GetFilmes(int paginas, int quantidadeFilmesPorPagina, List<int> generoIds, string ator, OrdenacaoAvaliacao ordenacaoAvaliacao)
        {
            var filmesFiltrados = await filmeRepository.GetFilmes(paginas, quantidadeFilmesPorPagina, generoIds, ator, ordenacaoAvaliacao);

            var listaDeFilmes = new ListaDeFilmesDto
            {
                Filmes = filmesFiltrados.Select(f => new FilmeDTO
                {
                    Nome = f.Nome,
                    AnoDeLancamento = f.AnoDeLancamento,
                    FaixaEtaria = f.FaixaEtaria,
                    GeneroId = f.Generos.Select(g => g.GeneroId).ToList(),
                    Atores = f.Atores,
                    Direcao = f.Direcao,
                    Duracao = f.Duracao,
                    Roteiristas = f.Roteiristas,
                    NotaMedia = f.NotaMedia,
                })
            };

            return listaDeFilmes;
        }
    }
}
