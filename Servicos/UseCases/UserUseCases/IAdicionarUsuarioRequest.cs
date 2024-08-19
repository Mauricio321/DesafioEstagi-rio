using Dominio.Models;
using FluentResults;
using MediatR;
using Servicos.Erros;
using Servicos.RepositoryInterfaces;
using Servicos.Services.ServiceInterfaces;
using System.Text.RegularExpressions;

namespace Servicos.UseCases.UserUseCases;


public interface IAdicionarUsuarioRequest : IRequest<Result>
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public int Role { get; }
}

public class AdicionarUsuarioRequest : IAdicionarUsuarioRequest
{
    private const int UserRoleId = 2;
    public int Role { get; } = UserRoleId;

    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
}

public class AdicionarAdministradorRequest : IAdicionarUsuarioRequest
{
    private const int AdminRoleId = 1;
    public int Role { get; } = AdminRoleId;

    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
}

public class AdicionarUsuarioBaseRequestHandler : IRequestHandler<AdicionarUsuarioRequest, Result>, IRequestHandler<AdicionarAdministradorRequest, Result>
{
    private readonly IUserRepository userRepository;
    private readonly IHashService hashService;
    public AdicionarUsuarioBaseRequestHandler(IUserRepository userRepository, IHashService hashService)
    {
        this.userRepository = userRepository;
        this.hashService = hashService;
    }

    public Task<Result> Handle(AdicionarUsuarioRequest request, CancellationToken cancellationToken)
    {
        return Handle((IAdicionarUsuarioRequest)request, cancellationToken);
    }

    public Task<Result> Handle(AdicionarAdministradorRequest request, CancellationToken cancellationToken)
    {
        return Handle((IAdicionarUsuarioRequest)request, cancellationToken);
    }

    private async Task<Result> Handle(IAdicionarUsuarioRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();

        var admin = new Usuario
        {
            Email = request.Email,
            Senha = hashService.HashPassword(request.Senha, out var salt),
            RoleId = request.Role,
            Salt = salt
        };

        var adminExistente = await userRepository.AdminExistente(request.Email);

        string Passwordpattern = @"[@#%&$]";
        var PasswordContainsSpecialChar = Regex.IsMatch(request.Senha, Passwordpattern);

        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        var containsemailPattern = Regex.IsMatch(request.Email, emailPattern);

        string passwordNumbers = @"\d";
        var passwordContainsNumber = Regex.IsMatch(request.Senha, passwordNumbers);

        string passwordCapitaLetter = @"[A-Z]";
        var passwordContainsCapitalLetter = Regex.IsMatch(request.Senha, passwordCapitaLetter);

        if (adminExistente)
        {
            return Result.Fail(new Forbiden("Conta ja cadastrada"));
        }

        if (!PasswordContainsSpecialChar)
        {
            return Result.Fail(new BadRequest("A senha deve conter um caractere especial"));
        }

        if (!containsemailPattern)
        {
            return Result.Fail(new BadRequest("Email invalido"));
        }

        if (!passwordContainsNumber)
        {
            return Result.Fail(new BadRequest("A senha deve conter numero"));
        }

        if (!passwordContainsCapitalLetter)
        {
            return Result.Fail(new BadRequest("A senha deve conter letra maiuscula"));
        }

        userRepository.AddUser(admin);
        userRepository.Savechanges();

        return Result.Ok();
    }
}
