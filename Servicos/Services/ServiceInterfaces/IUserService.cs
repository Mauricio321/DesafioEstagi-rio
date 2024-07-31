using Dominio.Models;
using Servicos.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Services.ServiceInterfaces
{
    public interface IUserService
    {
        string AddUser(UsuarioDTO usuario);
        string AddAdmin(AdministradorDTO administrador);
        string AuthUser(string email, string senha);
    }
}
