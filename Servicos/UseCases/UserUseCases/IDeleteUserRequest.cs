using MediatR;
using Servicos.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.UseCases.UserUseCases
{
    public class DeleteUserRequest : IRequest
    {
        public int id { get; set; }
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
            var usuario = await userRepository.FiltrarUsuarioPorId(request.id);

            userRepository.DeleteUsuario(usuario);

            userRepository.Savechanges();
        }
    }
}
