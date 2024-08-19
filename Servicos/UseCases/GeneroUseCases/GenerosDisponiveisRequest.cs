using Dominio.Models;
using MediatR;
using Servicos.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.UseCases.GeneroUseCases
{
    public class GenerosDisponiveisRequest : IRequest<IEnumerable<Genero>>
    {
    }

    public class GenerosDisponiveisRequestHandler : IRequestHandler<GenerosDisponiveisRequest, IEnumerable<Genero>>
    {
        private readonly IGeneroRepository generoRepository;
        public GenerosDisponiveisRequestHandler(IGeneroRepository generoRepository)
        {
            this.generoRepository = generoRepository;   
        }
        public Task<IEnumerable<Genero>> Handle(GenerosDisponiveisRequest request, CancellationToken cancellationToken)
        {
            return generoRepository.GenerosDisponiveis();
        }
    }
}
