using Dominio.Models;
using MediatR;
using Servicos.DTOs;
using Servicos.Interfaces;

namespace Servicos.UseCases.FilmeUseCases;

public class GetFilmesRequest : IRequest<ListaDeFilmesDto>
{
    public int paginas { get; set; }
    public int quantidadeFilmesPorPagina { get; set; }
    public List<int> generoIds { get; set; }
    public string ator { get; set; }

    public OrdenacaoAvaliacao OrdenacaoAvaliacao { get; set; }
}

public class GetFilmesRequestHandler : IRequestHandler<GetFilmesRequest, ListaDeFilmesDto>
{
    private readonly IFilmeRepository filmeRepository;
    public GetFilmesRequestHandler(IFilmeRepository filmeRepository)
    {
        this.filmeRepository = filmeRepository;
    }
    public async Task<ListaDeFilmesDto> Handle(GetFilmesRequest request, CancellationToken cancellationToken)
    {
        var filmesFiltrados = await filmeRepository.GetFilmes(request.paginas, request.quantidadeFilmesPorPagina, request.generoIds, request.ator, request.OrdenacaoAvaliacao);

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

