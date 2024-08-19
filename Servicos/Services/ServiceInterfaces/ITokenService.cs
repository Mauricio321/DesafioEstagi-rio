using Dominio.Models;

namespace Servicos.Services.ServiceInterfaces;

public interface ITokenService
{
    public string GenerateToken(Usuario user);
}

