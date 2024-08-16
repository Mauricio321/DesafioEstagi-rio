using MediatR;
using Servicos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.UseCases.FilmeUseCases
{
    public class DeleteFilmeRequest : IRequest
    {
        public int id { get; set; }
    }

    public class DeleteFilmeAsyncRequestHandler : IRequestHandler<DeleteFilmeRequest>
    {
        private readonly IFilmeRepository filmeRepository;
        public DeleteFilmeAsyncRequestHandler(IFilmeRepository filmeRepository)
        {
            this.filmeRepository = filmeRepository;
        }
        public async Task Handle(DeleteFilmeRequest request, CancellationToken cancellationToken)
        {
            var filme = await filmeRepository.FiltrarFilmePorIdAsync(request.id);

            filmeRepository.DeleteFilme(filme);

            await filmeRepository.SaveChangesAsync();
        }
    }
}
