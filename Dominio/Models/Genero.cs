using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Models
{
    public class Genero
    {
        public string Nome { get; set; }
        public int GeneroId { get; set; }
        public IEnumerable<FilmeGenero>? Filmes { get; set; }
    }
}
