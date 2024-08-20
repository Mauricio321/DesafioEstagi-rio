using FluentResults;
using MediatR;
using Servicos.Erros;
using Servicos.RepositoryInterfaces;
using Servicos.Services.ServiceInterfaces;

namespace Servicos.UseCases.UserUseCases;

public class AuthUserRequest : IRequest<Result<string>>
{
    public string Email { get; set; }
    public string Senha { get; set; }
}

public class AuthUserRequestHandler : IRequestHandler<AuthUserRequest, Result<string>>
{
    private readonly IUserRepository userRepository;
    private readonly IHashService hashService;
    private readonly ITokenService tokenService;
    public AuthUserRequestHandler(IUserRepository userRepository, IHashService hashService)
    {
        this.userRepository = userRepository;
        this.hashService = hashService;
    }

    public async Task<Result<string>> Handle(AuthUserRequest request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByEmail(request.Email, cancellationToken);

        if (user == null)
        {
            return Result.Fail(new BadRequest("Username or password is invalid"));
        }

        if (!hashService.ComparerHashes(request.Senha, user.Salt, user.Senha))
        {
            return Result.Fail(new BadRequest("Username or password is invalid"));
        }


        var token = tokenService.GenerateToken(user);
        return Result.Ok(token);
    }
}
