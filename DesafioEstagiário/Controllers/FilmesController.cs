﻿using DesafioEstagiário.IResultError;
using Dominio.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicos.DTOs;
using Servicos.UseCases.FilmeUseCases;
using System.Security.Claims;

namespace DesafioEstagiário.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        private readonly ISender sender;

        public FilmesController(ISender sender)
        {
            this.sender = sender;
        }

        [HttpPost]
        [Authorize(Roles = "manager")]
        public async Task<IResult> AddFilmes(AdicionarFilmeRequest request)
        {
            var result = await sender.Send(request);

            return ResultExtention.Serialize(result);
        }

        [HttpGet]
        public async Task<ListaDeFilmesDto> FilmesDispponiveis(int paginas, int quantidadeFilmesPorPagina, [FromQuery] List<int> generoIds, string? ator, OrdenacaoAvaliacao ordenacaoAvaliacao)
        {
            var filmesDisponiveis = await sender.Send(new GetFilmesRequest { Ator = ator, GeneroIds = generoIds, OrdenacaoAvaliacao = ordenacaoAvaliacao, Paginas = paginas, QuantidadeFilmesPorPagina = quantidadeFilmesPorPagina });

            return filmesDisponiveis;
        }

        [HttpDelete]
        [Authorize(Roles = "manager")]
        public async Task<IResult> DeletarFilmes(int id)
        {
            var result = await sender.Send(new DeleteFilmeRequest { Id = id });

            return ResultExtention.Serialize(result);
        }

        [HttpPost("Avaliacao")]
        [Authorize]
        public async Task<IResult> AdicionarAvaliacao(AvaliacaoFilmeRequest request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var id = int.Parse(identity!.FindFirst("userId")!.Value);

            var result = await sender.Send(new AvaliacaoFilmeRequest { Comentario = request.Comentario, FilmeId = request.FilmeId, Nota = request.Nota, UsuarioId = id });

            return ResultExtention.Serialize(result);
        }
    }
}
