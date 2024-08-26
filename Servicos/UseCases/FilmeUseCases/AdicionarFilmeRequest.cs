using Dominio.Models;
using FluentResults;
using FluentValidation;
using MediatR;
using Servicos.Interfaces;
using Servicos.RepositoryInterfaces;

namespace Servicos.UseCases.FilmeUseCases;

public class AdicionarFilmeRequest : IRequest<Result>
{
    public required string Nome { get; set; }
    public int FaixaEtaria { get; set; }
    public int Duracao { get; set; }
    public required string Direcao { get; set; }
    public required string AnoDeLancamento { get; set; }
    public required string Roteiristas { get; set; }
    public required string Atores { get; set; }
    public List<int> GeneroId { get; set; } = new List<int>();
    public float NotaMedia { get; set; }

    public class Validator : AbstractValidator<AdicionarFilmeRequest>
    {
        public Validator()
        {
            RuleFor(l => l.GeneroId).NotEmpty().WithMessage("Filme nao pode ser adicionado sem genero");
            RuleFor(l => l.Duracao).NotNull().NotEmpty();
            RuleFor(l => l.Nome).NotNull().NotEmpty().Length(2, 85);
        }
    }
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
        await filmeRepository.SaveChangesAsync();

        return Result.Ok();
    }
}
