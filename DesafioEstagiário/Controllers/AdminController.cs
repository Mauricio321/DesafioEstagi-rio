using DesafioEstagiário.IResultError;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicos.DTOs;
using Servicos.Erros;
using Servicos.Services.ServiceInterfaces;
using System.Security.Claims;

namespace DesafioEstagiário.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "manager")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService userService;
        public AdminController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IResult> AdicionarAdmin(AdministradorDTO administrador)
        {
            int roleIdAdmin = 1;

            var result = await userService.AddAdmin(administrador, roleIdAdmin);

            return ResultExtention.Serialize(result);
        }

        [HttpDelete("Delete-Usuarios")]
        public void DeleteOutrosUsuarios(int id)
        {
            userService.DeleteUser(id);
        }

        [HttpDelete("Delete-Me")]
        public void DeleteAdmin()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var id = int.Parse(identity!.FindFirst("userId")!.Value);

            userService.DeleteUser(id);
        }
    }
}




