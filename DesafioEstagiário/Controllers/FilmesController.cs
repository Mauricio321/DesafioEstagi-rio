using Dominio.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Servicos.DTOs;
using Servicos.Services;
using Servicos.Services.ServiceInterfaces;

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
        public ActionResult AddFilmes(FilmeDTO filme) 
        {
            filmeService.AdicionarFilmes(filme);

            return Ok();
        }

        [HttpGet]
        public ListaDeFilmes FilmesDispponiveis(int paginas, int quantidadeFilmesPorPagina, List<int> generoIds, string ator, OrdenacaoAvaliacao ordenacaoAvaliacao)
        {
            var filmesDisponiveis = filmeService.GetFilmes(paginas, quantidadeFilmesPorPagina, generoIds, ator, ordenacaoAvaliacao);

            return filmesDisponiveis;
        }

        [HttpDelete]
        public void DeletarFilmes(Filme filme) 
        {
            filmeService.DeleteFilme(filme);
        }
    }
}
