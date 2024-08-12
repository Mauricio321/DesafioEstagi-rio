using Dominio.Models;
using FluentResults;
using Servicos.DTOs;

namespace Servicos.Services.ServiceInterfaces
{
    public interface IUserService
    {
        Task<Result> AddUser(UsuarioDTO usuario);
        Task<Result> AddAdmin(AdministradorDTO administrador);
        Task<Result<string>> AuthUser(string email, string senha, CancellationToken cancellationToken);
        Task DeleteUser(int id);
        Task<IEnumerable<Usuario>> ObterTodosUsuarios();
    }
}
