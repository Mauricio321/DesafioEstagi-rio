﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Models
{
    public class Role
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public List<Usuario> Usuarios { get; set; } = default!;
    }
}
