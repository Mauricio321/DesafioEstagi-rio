using Dominio.Models;
using FluentResults;
using FluentValidation;
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

    public class Validator<TAdicionarUsuarioRequest> : AbstractValidator<TAdicionarUsuarioRequest>
        where TAdicionarUsuarioRequest : IAdicionarUsuarioRequest
    {
        public Validator()
        {
            RuleFor(a => a.Senha).MinimumLength(8).WithMessage("A senha deve conter numero e pelo menos 8 digitos")
                .Must(ValidatePassword).WithMessage("A senha deve conter um caractere especial")
                .Must(ValidateCapitalLetter).WithMessage("A senha deve conter letra maiuscula");

            RuleFor(e => e.Email).Must(ValidateEmail).WithMessage("Email invalido");

        }

        private static bool ValidatePassword(string password)
        {
            string Passwordpattern = @"[@#%&$]";

            return Regex.IsMatch(password, Passwordpattern);
        }

        private static bool ValidateCapitalLetter(string password)
        {
            string passwordCapitaLetter = @"[A-Z]";

            return Regex.IsMatch(password, passwordCapitaLetter);
        }

        private static bool ValidateEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }

    }
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
    public IEnumerable<string> Permissions { get; set; } = Enumerable.Empty<string>();

    public class Validator : AbstractValidator<AdicionarAdministradorRequest>
    {
        public Validator()
        {
            RuleFor(a => a.Permissions).NotEmpty();
        }
    }
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

        var admin = new Usuario
        {
            Email = request.Email,
            Senha = hashService.HashPassword(request.Senha, out var salt),
            RoleId = request.Role,
            Nome = request.Nome,
            Salt = salt
        };

        var adminExistente = await userRepository.AdminExistente(request.Email);

        if (adminExistente)
        {
            return Result.Fail(new Forbiden("Conta ja cadastrada"));
        }

        await userRepository.AddUser(admin);
        await userRepository.Savechanges();

        return Result.Ok();
    }
}
