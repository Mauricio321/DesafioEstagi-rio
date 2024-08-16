using Dominio.Models;
using MediatR;
using Servicos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.UseCases.FilmeUseCases
{
    public class GetAvaliacoesFilmeRequest : IRequest<List<Avaliacao>>
    {
        public int id { get; set; }
    }

    public class GetAvaliacoesFilmeRequestHandler : IRequestHandler<GetAvaliacoesFilmeRequest, List<Avaliacao>>
    {
        private readonly IFilmeRepository filmeRepository;
        public GetAvaliacoesFilmeRequestHandler(IFilmeRepository filmeRepository)
        {
            this.filmeRepository = filmeRepository;
        }
        public async Task<List<Avaliacao>> Handle(GetAvaliacoesFilmeRequest request, CancellationToken cancellationToken)
        {
            var avaliacao = await filmeRepository.FiltrarFilmePorId(request.id);

            return avaliacao.Avaliacoes.ToList();
        }
    }
}
