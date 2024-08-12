using Dominio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicos.DTOs;
using Servicos.Erros;
using Servicos.Services.ServiceInterfaces;
using System.Security.Claims;

namespace DesafioEstagiário.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUserService userService;
        public UsuarioController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarUsuario(UsuarioDTO usuario)
        {
            var result = await userService.AddUser(usuario);

            if (result.IsFailed)
            {
                if (result.Errors[0] is Forbiden)
                {
                    return StatusCode(403, result.Errors[0].Message);
                }
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
