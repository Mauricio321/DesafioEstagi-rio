using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.DTOs
{
    public class AvaliacaoDTO
    {
        public int Nota { get; set; }
        public int FilmeId { get; set; }
        public string? Comentario { get; set; } 
    }
}
