using FluentResults;
using FluentValidation;
using MediatR;
using Servicos.Erros;
using Servicos.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.UseCases.GeneroUseCases
{
    public class DeleteGeneroRequest : IRequest<Result>
    {
        public int Id { get; set; }

        public class DeleteGeneroValidator : AbstractValidator<DeleteGeneroRequest>
        {
            public DeleteGeneroValidator()
            {
                RuleFor(d => d.Id).NotNull().NotEmpty();
            }
        }
    }

    public class DeleteGeneroRequestHandler : IRequestHandler<DeleteGeneroRequest, Result>
    {
        private readonly IGeneroRepository generoRepository;
        public DeleteGeneroRequestHandler(IGeneroRepository generoRepository)
        {
            this.generoRepository = generoRepository;
        }
        public async Task<Result> Handle(DeleteGeneroRequest request, CancellationToken cancellationToken = default)
        {
            var genero = await generoRepository.GetGenero(request.Id);

            if (genero is null)
                return new NaoEncontrado("Genero nao encontrado");

            generoRepository.DeleteGenero(genero);

            await generoRepository.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
