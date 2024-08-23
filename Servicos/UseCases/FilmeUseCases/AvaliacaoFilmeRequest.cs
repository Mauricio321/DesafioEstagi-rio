using Dominio.Models;
using FluentResults;
using FluentValidation;
using MediatR;
using Servicos.Interfaces;

namespace Servicos.UseCases.FilmeUseCases
{
    public class AvaliacaoFilmeRequest : IRequest<Result>
    {
        public int UsuarioId { get; set; }

        public int Nota { get; set; }
        public int FilmeId { get; set; }
        public string? Comentario { get; set; }
        public class Validator : AbstractValidator<AvaliacaoFilmeRequest>
        {
            public Validator()
            {
                RuleFor(l => l.Nota).LessThanOrEqualTo(5).WithMessage("a nota max é 5");
                RuleFor(f => f.FilmeId).NotEmpty().NotNull();
                RuleFor(u => u.UsuarioId).NotEmpty().NotNull();
            }
        }
    }



    public class AvaliacaoFilmeRequestHandler : IRequestHandler<AvaliacaoFilmeRequest, Result>
    {
        private readonly IFilmeRepository filmeRepository;

        public AvaliacaoFilmeRequestHandler(IFilmeRepository filmeRepository)
        {
            this.filmeRepository = filmeRepository;
        }

        public async Task<Result> Handle(AvaliacaoFilmeRequest request, CancellationToken cancellationToken)
        {
            var avaliacao = await filmeRepository.UsuarioJaAvaliou(request.FilmeId, request.UsuarioId);

            if (avaliacao is not null)
            {
                avaliacao.Nota = request.Nota;

                filmeRepository.AtualizarAvaliacao(avaliacao);
                await filmeRepository.SaveChangesAsync();

                return Result.Ok();
            }

            var avaliacoes = new Avaliacao
            {
                FilmeId = request.FilmeId,
                Nota = request.Nota,
                Comentario = request.Comentario,
                UsuarioId = request.UsuarioId
            };

            filmeRepository.AvaliacaoFilme(avaliacoes);
            await filmeRepository.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
