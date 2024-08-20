using DesafioEstagiário.IResultError;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Authorize(Roles = "manager")]
    public class AdminController : ControllerBase
    {
        private readonly ISender sender;
        public AdminController(ISender sender)
        {
            this.sender = sender;
        }

        [HttpPost]
        public async Task<IResult> AdicionarAdmin(AdicionarAdministradorRequest request)
        {

            var result = await sender.Send(new AdicionarAdministradorRequest { Nome = request.Nome, Email = request.Email, Senha = request.Senha });

            return ResultExtention.Serialize(result);
        }

        [HttpDelete("Delete-Usuarios")]
        public void DeleteOutrosUsuarios(int id)
        {
            sender.Send(new DeleteUserRequest { Id = id });
        }

        [HttpDelete("Delete-Me")]
        public void DeleteAdmin()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var id = int.Parse(identity!.FindFirst("userId")!.Value);

            sender.Send(new DeleteUserRequest { Id = id });
        }
    }
}




