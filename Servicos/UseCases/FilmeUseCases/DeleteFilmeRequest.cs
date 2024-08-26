using FluentResults;
using FluentValidation;
using MediatR;
using Servicos.Erros;
using Servicos.Interfaces;

namespace Servicos.UseCases.FilmeUseCases
{
    public class DeleteFilmeRequest : IRequest<Result>
    {
        public int Id { get; set; }
    }
    public class DeleteFilmeAsyncRequestHandler : IRequestHandler<DeleteFilmeRequest, Result>
    {
        private readonly IFilmeRepository filmeRepository;
        public DeleteFilmeAsyncRequestHandler(IFilmeRepository filmeRepository)
        {
            this.filmeRepository = filmeRepository;
        }
        public async Task<Result> Handle(DeleteFilmeRequest request, CancellationToken cancellationToken)
        {
            var filme = await filmeRepository.FiltrarFilmePorIdAsync(request.Id);

            if (filme is null)
                return new NaoEncontrado("Filme nao encontrado");

            filmeRepository.DeleteFilme(filme);

            await filmeRepository.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
