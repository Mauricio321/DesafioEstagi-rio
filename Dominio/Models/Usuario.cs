using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public int RoleId { get; set; }
        public byte[] Salt { get; set; } = Array.Empty<byte>();
        public Role Role { get; set; } = default!;
    }
}
