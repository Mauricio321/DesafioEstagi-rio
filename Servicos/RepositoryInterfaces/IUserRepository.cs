using Dominio.Models;

namespace Servicos.RepositoryInterfaces;

public interface IUserRepository
{
    void AddUser(Usuario usuario);
    Task<bool> UsuarioExistente(string email);
    Task<bool> AdminExistente(string email);
    Task<Usuario?> GetUserByEmail(string email, CancellationToken cancellationToken);
    void DeleteUsuario(Usuario usuario);
    Task<Usuario> FiltrarUsuarioPorId(int id);
    void Savechanges();
    Task<IEnumerable<Usuario>> ObterTodosUsuarios();
}
