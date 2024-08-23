using FluentValidation;
using MediatR;
using Servicos.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.UseCases.GeneroUseCases
{
    public class DeleteGeneroRequest : IRequest
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

    public class DeleteGeneroRequestHandler : IRequestHandler<DeleteGeneroRequest>
    {
        private readonly IGeneroRepository generoRepository;
        public DeleteGeneroRequestHandler(IGeneroRepository generoRepository)
        {
            this.generoRepository = generoRepository;
        }
        public Task Handle(DeleteGeneroRequest request, CancellationToken cancellationToken)
        {
            generoRepository.DeleteGenero(request.Id);

            generoRepository.SaveChangesAsync();

            return Task.CompletedTask;
        }
    }
}
