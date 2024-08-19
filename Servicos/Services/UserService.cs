using Dominio.Models;
using FluentResults;
using Servicos.DTOs;
using Servicos.Erros;
using Servicos.RepositoryInterfaces;
using Servicos.Services.ServiceInterfaces;
using System.Text.RegularExpressions;

namespace Servicos.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IHashService hashService;
    private readonly ITokenService tokenService;

    public UserService(IUserRepository userRepository, IHashService hashService, ITokenService tokenService)
    {
        this.userRepository = userRepository;
        this.hashService = hashService;
        this.tokenService = tokenService;
    }

    public async Task<Result> AddAdmin(AdministradorDTO administrador, int roleIdUser)
    {
        var admin = new Usuario
        {
            Email = administrador.Email,
            Senha = hashService.HashPassword(administrador.Senha, out var salt),
            RoleId = roleIdUser,
            Salt = salt
        };

        var adminExistente = await userRepository.AdminExistente(administrador.Email);

        string Passwordpattern = @"[@#%&$]";
        var PasswordContainsSpecialChar = Regex.IsMatch(administrador.Senha, Passwordpattern);

        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        var containsemailPattern = Regex.IsMatch(administrador.Email, emailPattern);

        string passwordNumbers = @"\d";
        var passwordContainsNumber = Regex.IsMatch(administrador.Senha, passwordNumbers);

        string passwordCapitaLetter = @"[A-Z]";
        var passwordContainsCapitalLetter = Regex.IsMatch(administrador.Senha, passwordCapitaLetter);

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

    public async Task<Result<string>> AuthUser(string email, string senha, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByEmail(email, cancellationToken);


        if (user == null)
        {
            return Result.Fail(new BadRequest("Username or password is invalid"));
        }

        if (!hashService.ComparerHashes(senha, user.Salt, user.Senha))
        {
            return Result.Fail(new BadRequest("Username or password is invalid"));
        }

        var token = tokenService.GenerateToken(user);

        return Result.Ok(token);
    }

    public async Task DeleteUser(int id)
    {
        var usuario = await userRepository.FiltrarUsuarioPorId(id);

        userRepository.DeleteUsuario(usuario);

        userRepository.Savechanges();
    }

    public Task<IEnumerable<Usuario>> ObterTodosUsuarios()
    {
        return userRepository.ObterTodosUsuarios();
    }
}
