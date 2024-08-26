using DesafioEstagiário.IResultError;
using Dominio.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicos.UseCases.UserUseCases;
using System.Security.Claims;

namespace DesafioEstagiário.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ISender sender;

        public UsuarioController(ISender sender)
        {
            this.sender = sender;
        }

        [HttpPost]
        public async Task<IResult> AdicionarUsuario(AdicionarUsuarioRequest request, CancellationToken cancellationToken)
        {
            var result = await sender.Send(request, cancellationToken);

            return ResultExtention.Serialize(result);

        }

        [HttpDelete]
        [Authorize]
        public async Task DeleteUsuario()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var id = int.Parse(identity!.FindFirst("userId")!.Value);

            await sender.Send(new DeleteUserRequest { Id = id });
        }

        [HttpGet]
        [Authorize(Roles = "manager")]
        public async Task<IEnumerable<Usuario>> ObterTodosUsuarios()
        {
            return await sender.Send(new ObterTodosUsuariosRequest());
        }
    }
}
