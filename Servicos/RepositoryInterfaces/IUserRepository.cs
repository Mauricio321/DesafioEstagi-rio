using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.RepositoryInterfaces
{
    public interface IUserRepository
    {
        void AddUser(Usuario usuario);
    }
}
