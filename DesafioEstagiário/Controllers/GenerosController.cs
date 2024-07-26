using Dominio.Models;
using Infraestrutura.Repositories;
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
        public void AddGenero(GeneroDTO genero)
        {
            generoService.AddGenero(genero);    
        }

        [HttpGet]
        public void GeneroDisponiveis()
        {
        }

        [HttpDelete]
        public void DeleteGeneros(int id)
        {
            generoService.DeleteGenero(id);
        }
    }
}
