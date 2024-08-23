using DesafioEstagiário.IResultError;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Servicos.DTOs;
using Servicos.RepositoryInterfaces;
using Servicos.UseCases.UserUseCases;

namespace DesafioEstagiário.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AutorizacaoController : ControllerBase
{
    private readonly IUserRepository userRepository;
    private readonly ISender sender;

    public AutorizacaoController(IUserRepository userRepository, ISender sender)
    {
        this.userRepository = userRepository;
        this.sender = sender;
    }

    [HttpPost]
    public async Task<IResult> AutorizacaoUsuario([FromBody] UsuarioSemNomeDTO usuario, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new AuthUserRequest { Email = usuario.Email, Senha = usuario.Senha }, cancellationToken);

        return ResultExtention.Serialize(result);
    }
}
