using Dominio.Models;
using FluentValidation;
using MediatR;
using Servicos.RepositoryInterfaces;
using Servicos.UseCases.FilmeUseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.UseCases.GeneroUseCases
{
    public class GetGenerosRequest : IRequest<IEnumerable<Genero>>
    {
        public List<int> Id { get; set; }   

        public class GetGenerosValidator : AbstractValidator<GetGenerosRequest> 
        {
            public GetGenerosValidator() 
            {
                RuleFor(g => g.Id).NotEmpty().NotNull();
            }
        }
    }

    public class GetGenerosRequestHandler : IRequestHandler<GetGenerosRequest, IEnumerable<Genero>>
    {
        private readonly IGeneroRepository generoRepository;
        public GetGenerosRequestHandler(IGeneroRepository generoRepository)
        {
            this.generoRepository = generoRepository;
        }
        public async Task<IEnumerable<Genero>> Handle(GetGenerosRequest request, CancellationToken cancellationToken)
        {
            var genero = await generoRepository.GetGeneros(request.Id);

            return genero;
        }
    }
}
