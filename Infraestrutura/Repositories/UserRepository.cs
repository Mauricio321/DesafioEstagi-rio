using Dominio.Models;
using Infraestrutura.Data;
using Servicos.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FilmeContext context;
        public UserRepository(FilmeContext context)
        {
            this.context = context;
        }

        public void AddUser(Usuario usuario)
        {
            context.Usuarios.Add(usuario);
        }
    }
}
