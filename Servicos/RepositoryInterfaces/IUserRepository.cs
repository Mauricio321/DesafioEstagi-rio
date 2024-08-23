using Dominio.Models;

namespace Servicos.RepositoryInterfaces;

public interface IUserRepository
{
    Task AddUser(Usuario usuario);
    Task<bool> UsuarioExistente(string email);
    Task<bool> AdminExistente(string email);
    Task<Usuario?> GetUserByEmail(string email, CancellationToken cancellationToken);
    void DeleteUsuario(Usuario usuario);
    Task<Usuario?> FiltrarUsuarioPorId(int id);
    Task Savechanges();
    Task<IEnumerable<Usuario>> ObterTodosUsuarios();
}
