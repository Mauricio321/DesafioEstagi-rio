using Dominio.Models;
using MediatR;
using Servicos.Interfaces;

namespace Servicos.UseCases.FilmeUseCases
{
    public class AvaliacaoFilmeRequest : IRequest<Avaliacao>
    {
        public int UsuarioId { get; set; }

        public int Nota { get; set; }
        public int FilmeId { get; set; }
        public string? Comentario { get; set; }
    }

    public class AvaliacaoFilmeRequestHandler : IRequestHandler<AvaliacaoFilmeRequest, Avaliacao>
    {
        private readonly IFilmeRepository filmeRepository;
        public AvaliacaoFilmeRequestHandler(IFilmeRepository filmeRepository)
        {
            this.filmeRepository = filmeRepository;
        }

        public async Task<Avaliacao> Handle(AvaliacaoFilmeRequest request, CancellationToken cancellationToken)
        {
            var avaliacao = await filmeRepository.UsuarioJaAvaliou(request.FilmeId, request.UsuarioId);

            if (avaliacao is not null)
            {
                avaliacao.Nota = request.Nota;

                filmeRepository.AtualizarAvaliacao(avaliacao);
                await filmeRepository.SaveChangesAsync();

                return avaliacao;
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

            return avaliacoes;
        }
    }
}
