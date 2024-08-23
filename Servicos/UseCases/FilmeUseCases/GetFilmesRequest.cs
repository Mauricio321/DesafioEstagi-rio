using Dominio.Models;
using FluentValidation;
using MediatR;
using Servicos.DTOs;
using Servicos.Interfaces;

namespace Servicos.UseCases.FilmeUseCases;

public class GetFilmesRequest : IRequest<ListaDeFilmesDto>
{
    public int Paginas { get; set; }
    public int QuantidadeFilmesPorPagina { get; set; }
    public List<int> GeneroIds { get; set; } = new List<int>();
    public string? Ator { get; set; }

    public OrdenacaoAvaliacao OrdenacaoAvaliacao { get; set; }

    public class GetFilmesValidator : AbstractValidator<GetFilmesRequest> 
    {
        public GetFilmesValidator() 
        {
            RuleFor(g => g.GeneroIds).NotNull().NotEmpty();
        } 
    }
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
        var filmesFiltrados = await filmeRepository.GetFilmes(request.Paginas, request.QuantidadeFilmesPorPagina, request.GeneroIds, request.Ator, request.OrdenacaoAvaliacao);

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

