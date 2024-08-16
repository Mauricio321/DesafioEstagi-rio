using Dominio.Models;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicos.DTOs;
using Servicos.Erros;
using Servicos.Services.ServiceInterfaces;
using Servicos.UseCases.UserUseCases;
using System.Security.Claims;

namespace DesafioEstagiário.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ISender sender;

        public UsuarioController(IUserService userService, ISender sender)
        {
            this.userService = userService;
            this.sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarUsuario(AdicionarUsuarioRequest request, CancellationToken cancellationToken)
        {
            var result = await sender.Send(request, cancellationToken);

            if (result.IsFailed)
            {
                if (result.Errors[0] is Forbiden)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, result.Errors[0].Message);
                }
                if (result.Errors[0] is BadRequest) 
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result.Errors[0].Message);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Algum erro ocorreu no servidor, ligue para a central");
            }

            return NoContent();

        }

        [HttpDelete]
        [Authorize]
        public void DeleteUsuario()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var id = int.Parse(identity!.FindFirst("userId")!.Value);

            userService.DeleteUser(id);
        }

        [HttpGet]
        [Authorize(Roles = "manager")]
        public Task<IEnumerable<Usuario>> ObterTodosUsuarios()
        {
            return userService.ObterTodosUsuarios();
        }
    }
}
