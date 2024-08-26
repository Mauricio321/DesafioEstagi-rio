namespace Dominio.Models
{
    public class FilmeGenero
    {
        public Filme Filme { get; set; } = default!;
        public int FilmeId { get; set; }
        public Genero Genero { get; set; } = default!;
        public int GeneroId { get; set; }
    }
}
