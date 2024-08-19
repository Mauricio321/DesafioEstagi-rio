using Dominio.Models;
using Infraestrutura.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicos.DTOs;
using Servicos.Services.ServiceInterfaces;

namespace DesafioEstagiário.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly IGeneroService generoService;
        public GenerosController(IGeneroService generoService)
        {
            this.generoService = generoService;
        }

        [HttpPost]
        [Authorize(Roles = "manager")]
        public async Task AddGenero(GeneroDTO genero)
        {
            await generoService.AddGenero(genero);
        }

        [HttpGet]
        public async Task<IEnumerable<Genero>> GeneroDisponiveis()
        {
            return await generoService.GenerosDisponiveis();
        }

        [HttpDelete]
        [Authorize(Roles = "manager")]
        public void DeleteGeneros(int id)
        {
            generoService.DeleteGenero(id);
        }
    }
}
