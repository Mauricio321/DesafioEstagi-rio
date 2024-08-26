using FluentResults;
using FluentValidation;
using MediatR;
using Servicos.Erros;
using Servicos.RepositoryInterfaces;

namespace Servicos.UseCases.UserUseCases
{
    public class DeleteUserRequest : IRequest<Result>
    {
        public int Id { get; set; }

        public class DeleteUserValidator : AbstractValidator<DeleteUserRequest> 
        {
            public DeleteUserValidator() 
            {
                RuleFor(d => d.Id).NotNull().NotEmpty();
            }    
        }
    }

    public class DeleteUserRequestHandler : IRequestHandler<DeleteUserRequest, Result>
    {
        private readonly IUserRepository userRepository;
        public DeleteUserRequestHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<Result> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var usuario = await userRepository.FiltrarUsuarioPorId(request.Id);

            if (usuario is null)
                return new NaoEncontrado("Usuario nao encontrado");

            userRepository.DeleteUsuario(usuario);

            await userRepository.Savechanges();

            return Result.Ok();
        }
    }
}
