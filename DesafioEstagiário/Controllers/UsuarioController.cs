using DesafioEstagiário.IResultError;
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

           await userService.DeleteUser(id);
        }

        [HttpGet]
        [Authorize(Roles = "manager")]
        public async Task<IEnumerable<Usuario>> ObterTodosUsuarios()
        {
            return await userService.ObterTodosUsuarios();
        }
    }
}
