using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Services.ServiceInterfaces
{
    public interface IHashService
    {
        string HashPassword(string password, out byte[] salt);
        string HashPassword(string password, byte[] salt);
        bool ComparerHashes(string password, byte[] salt, string hashedPasswordToComparer);
    }
}
