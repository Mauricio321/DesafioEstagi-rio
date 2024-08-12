﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Models
{
    public class Administrador
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int RoleId {  get; set; }
        public Role Role { get; set; } = default;
        public byte[] Salt { get; set; } = Array.Empty<byte>();
    }
}
