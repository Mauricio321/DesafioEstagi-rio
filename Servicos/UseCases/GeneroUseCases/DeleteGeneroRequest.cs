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

            generoRepository.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
