using Dominio.Models;
using MediatR;
using Servicos.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.UseCases.UserUseCases
{
    public class ObterTodosUsuariosRequest : IRequest<IEnumerable<Usuario>>
    {
    }

    public class ObterTodosUsuariosRequestHandler : IRequestHandler<ObterTodosUsuariosRequest, IEnumerable<Usuario>>
    {
        private readonly IUserRepository userRepository;
        public ObterTodosUsuariosRequestHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public Task<IEnumerable<Usuario>> Handle(ObterTodosUsuariosRequest request, CancellationToken cancellationToken)
        {
            return userRepository.ObterTodosUsuarios();
        }
    }
}
