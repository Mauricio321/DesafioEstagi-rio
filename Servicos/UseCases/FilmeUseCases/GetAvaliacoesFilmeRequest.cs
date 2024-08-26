using Dominio.Models;
using FluentResults;
using FluentValidation;
using MediatR;
using Servicos.Erros;
using Servicos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.UseCases.FilmeUseCases
{
    public class GetAvaliacoesFilmeRequest : IRequest<Result<List<Avaliacao>>>
    {
        public int id { get; set; }

        public class GetAvaliacaoValidaotor : AbstractValidator<GetAvaliacoesFilmeRequest> 
        {
            public GetAvaliacaoValidaotor()
            {
                RuleFor(g => g.id).NotNull().NotEmpty();
            }
        }
    }

    public class GetAvaliacoesFilmeRequestHandler : IRequestHandler<GetAvaliacoesFilmeRequest, Result<List<Avaliacao>>>
    {
        private readonly IFilmeRepository filmeRepository;
        public GetAvaliacoesFilmeRequestHandler(IFilmeRepository filmeRepository)
        {
            this.filmeRepository = filmeRepository;
        }
        public async Task<Result<List<Avaliacao>>> Handle(GetAvaliacoesFilmeRequest request, CancellationToken cancellationToken)
        {
            var avaliacao = await filmeRepository.FiltrarFilmePorId(request.id);

            if (avaliacao is null)
                return new NaoEncontrado("Filme nao encontrado");

            return avaliacao.Avaliacoes.ToList();
        }
    }
}
