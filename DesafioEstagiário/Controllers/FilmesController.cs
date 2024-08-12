using Dominio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Servicos.DTOs;
using Servicos.Services;
using Servicos.Services.ServiceInterfaces;
using System.Security.Claims;

namespace DesafioEstagiário.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        private readonly IFilmeService filmeService;

        public FilmesController(IFilmeService filmeService)
        {
            this.filmeService = filmeService;
        }

        [HttpPost]
        [Authorize(Roles = "manager")] 
        public async Task<ActionResult> AddFilmes(FilmeDTO filme)
        {
            var result = await filmeService.AdicionarFilmes(filme);

            if(result.IsFailed)
                return NotFound(result.Errors.First().Message);

            return Ok();
        }

        [HttpGet]
        public Task<ListaDeFilmesDto> FilmesDispponiveis(int paginas, int quantidadeFilmesPorPagina, [FromQuery] List<int> generoIds, string? ator, OrdenacaoAvaliacao ordenacaoAvaliacao)
        {
            var filmesDisponiveis = filmeService.GetFilmes(paginas, quantidadeFilmesPorPagina, generoIds, ator, ordenacaoAvaliacao);

            return filmesDisponiveis;
        }

        [HttpDelete]
        [Authorize(Roles = "manager")]
        public async Task DeletarFilmes(int id)
        {
            await filmeService.DeleteFilmeAsync(id);
        }

        [HttpPost("Avaliacao")]
        [Authorize]
        public Task<Avaliacao> AdicionarAvaliacao(AvaliacaoDTO avaliacao)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var id = int.Parse(identity!.FindFirst("userId")!.Value);

            var avaliacoes = filmeService.AvaliacaoFilme(avaliacao, id);

            return avaliacoes;
        }
    }
}
