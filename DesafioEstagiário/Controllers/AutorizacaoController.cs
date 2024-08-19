using DesafioEstagiário.IResultError;
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
    public async Task<IResult> AutorizacaoUsuario([FromBody] UsuarioSemNomeDTO usuario, CancellationToken cancellationToken)
    {
        var result = await userService.AuthUser(usuario.Email, usuario.Senha, cancellationToken);

        return ResultExtention.Serialize(result);
    }
}
