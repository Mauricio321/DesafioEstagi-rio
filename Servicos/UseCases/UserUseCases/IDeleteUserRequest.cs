using MediatR;
using Servicos.RepositoryInterfaces;

namespace Servicos.UseCases.UserUseCases
{
    public class DeleteUserRequest : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteUserRequestHandler : IRequestHandler<DeleteUserRequest>
    {
        private readonly IUserRepository userRepository;
        public DeleteUserRequestHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var usuario = await userRepository.FiltrarUsuarioPorId(request.Id);

            userRepository.DeleteUsuario(usuario);

            userRepository.Savechanges();
        }
    }
}
