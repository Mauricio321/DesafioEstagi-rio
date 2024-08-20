using DesafioEstagiário.IResultError;
using Dominio.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicos.UseCases.GeneroUseCases;

namespace DesafioEstagiário.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly ISender sender;
        public GenerosController(ISender sender)
        {
            this.sender = sender;
        }

        [HttpPost]
        [Authorize(Roles = "manager")]
        public async Task<IResult> AddGenero(AdicionarGeneroRequest request, CancellationToken cancellationToken)
        {
            var result = await sender.Send(request, cancellationToken);

            return ResultExtention.Serialize(result);
        }

        [HttpGet]
        public async Task<IEnumerable<Genero>> GeneroDisponiveis()
        {
            return await sender.Send(new GenerosDisponiveisRequest());
        }

        [HttpDelete]
        [Authorize(Roles = "manager")]
        public void DeleteGeneros(int id)
        {
            sender.Send(new DeleteGeneroRequest { Id = id });
        }
    }
}
