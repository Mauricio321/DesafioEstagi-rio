using Dominio.Models;
using Infraestrutura.Data;
using Microsoft.EntityFrameworkCore;
using Servicos.RepositoryInterfaces;

namespace Infraestrutura.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FilmeContext context;
        public UserRepository(FilmeContext context)
        {
            this.context = context;
        }

        public async Task AddUser(Usuario usuario)
        {
            await context.Usuarios.AddAsync(usuario);
        }

        public Task<bool> UsuarioExistente(string email)
        {
            return context.Usuarios.AnyAsync(u => u.Email == email);
        }

        public Task<bool> AdminExistente(string email)
        {
            return context.Usuarios.AnyAsync(u => u.Email == email);
        }

        public async Task Savechanges()
        {
            await context.SaveChangesAsync();
        }

        public async Task<Usuario?> GetUserByEmail(string email, CancellationToken cancellationToken)
        {
            return await context.Usuarios.Include(r => r.Role).FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public void DeleteUsuario(Usuario usuario)
        {
            context.Usuarios.Remove(usuario);
        }

        public Task<Usuario?> FiltrarUsuarioPorId(int id)
        {
            return context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Usuario>> ObterTodosUsuarios()
        {
            return await context.Usuarios.OrderBy(u => u.Nome).ToListAsync();
        }
    }
}
