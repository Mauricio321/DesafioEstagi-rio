using Dominio.Models;
using FluentResults;
using FluentValidation;
using MediatR;
using Servicos.RepositoryInterfaces;

namespace Servicos.UseCases.GeneroUseCases;

public class AdicionarGeneroRequest : IRequest<Result>
{
    public required string Nome { get; set; }

    public class AdicionarGeneroValidator : AbstractValidator<AdicionarGeneroRequest> 
    {
        public AdicionarGeneroValidator() 
        {
            RuleFor(a => a.Nome).NotEmpty().NotEmpty();
        }   
    }
}

public class AdicionarGeneroRequestHandler : IRequestHandler<AdicionarGeneroRequest, Result>
{
    private readonly IGeneroRepository generoRepository;

    public AdicionarGeneroRequestHandler(IGeneroRepository generoRepository)
    {
        this.generoRepository = generoRepository;
    }

    public async Task<Result> Handle(AdicionarGeneroRequest request, CancellationToken cancellationToken)
    {
        var generos = new Genero
        {
            Nome = request.Nome
        };

        await generoRepository.AddGenero(generos);

        await generoRepository.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
