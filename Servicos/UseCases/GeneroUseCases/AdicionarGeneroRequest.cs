using Dominio.Models;
using MediatR;
using Servicos.RepositoryInterfaces;

namespace Servicos.UseCases.GeneroUseCases
{
    public class AdicionarGeneroRequest : IRequest
    {
        public string Nome { get; set; }
    }

    public class AdicionarGeneroRequestHandler : IRequestHandler<AdicionarGeneroRequest>
    {
        private readonly IGeneroRepository generoRepository;
        public AdicionarGeneroRequestHandler(IGeneroRepository generoRepository)
        {
            this.generoRepository = generoRepository;
        }
        public Task Handle(AdicionarGeneroRequest request, CancellationToken cancellationToken)
        {
            var generos = new Genero
            {
                Nome = request.Nome
            };

            generoRepository.AddGenero(generos);

            generoRepository.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
