using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicos.DTOs;
using Servicos.RepositoryInterfaces;
using Servicos.Services.ServiceInterfaces;

namespace DesafioEstagiário.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorizacaoController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IUserService userService;

        public AutorizacaoController(IUserRepository userRepository, IUserService userService)
        {
            this.userRepository = userRepository;
            this.userService = userService;
        }

        [HttpPost]
        public IActionResult AutorizacaoUsuario([FromBody] UsuarioSemNomeDTO usuario, CancellationToken cancellationToken)
        {
            var token = userService.AuthUser(usuario.Email, usuario.Senha, cancellationToken);

            return Ok(token);
        }
    }
}
