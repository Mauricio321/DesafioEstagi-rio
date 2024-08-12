using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicos.DTOs;
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
        public async Task<Result> AdicionarAdmin(AdministradorDTO administrador)
        {
            return await userService.AddAdmin(administrador);
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




