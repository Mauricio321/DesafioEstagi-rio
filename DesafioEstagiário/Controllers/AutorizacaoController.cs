using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Servicos.DTOs;
using Servicos.Erros;
using Servicos.RepositoryInterfaces;
using Servicos.Services.ServiceInterfaces;

namespace DesafioEstagiário.Controllers;

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
    public async Task<IActionResult> AutorizacaoUsuario([FromBody] UsuarioSemNomeDTO usuario, CancellationToken cancellationToken)
    {
        var result = await userService.AuthUser(usuario.Email, usuario.Senha, cancellationToken);

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

        return Ok(result.Value);
    }
}
