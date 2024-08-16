using Dominio.Models;
using FluentResults;
using MediatR;
using Servicos.Interfaces;
using Servicos.RepositoryInterfaces;

namespace Servicos.UseCases.FilmeUseCases;

public class AdicionarFilmeRequest : IRequest<Result>
{
    public string? Nome { get; set; }
    public int FaixaEtaria { get; set; }
    public int Duracao { get; set; }
    public string? Direcao { get; set; }
    public string? AnoDeLancamento { get; set; }
    public string? Roteiristas { get; set; }
    public string? Atores { get; set; }
    public List<int> GeneroId { get; set; }
    public float NotaMedia { get; set; }
}

public class AdicionarFilmeRequestHandler : IRequestHandler<AdicionarFilmeRequest, Result>
{
    private readonly IFilmeRepository filmeRepository;
    private readonly IGeneroRepository generoRepository;
    public AdicionarFilmeRequestHandler(IFilmeRepository filmeRepository, IGeneroRepository generoRepository)
    {
        this.filmeRepository = filmeRepository;
        this.generoRepository = generoRepository;
    }
    public async Task<Result> Handle(AdicionarFilmeRequest request, CancellationToken cancellationToken)
    {
        var genero = await generoRepository.GetGeneros(request.GeneroId);

        var generoNaoEncontrado = request.GeneroId.Where(generoId => !genero.Any(generos => generos.GeneroId == generoId));

        if (generoNaoEncontrado.Any())
        {
            return Result.Fail("Genero não encontrado");
        }

        var generos = genero.Select(genero =>
        new FilmeGenero
        {
            Genero = genero
        });

        var filmes = new Filme
        {
            Nome = request.Nome,
            Duracao = request.Duracao,
            FaixaEtaria = request.FaixaEtaria,
            AnoDeLancamento = request.AnoDeLancamento,
            Atores = request.Atores,
            Direcao = request.Direcao,
            Roteiristas = request.Roteiristas,
            Generos = generos.ToList(),
        };

        filmeRepository.AdicionarFilmes(filmes);
        filmeRepository.SaveChanges();

        return Result.Ok();
    }
}
