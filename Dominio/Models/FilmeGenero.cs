namespace Dominio.Models
{
    public class FilmeGenero
    {
        public Filme Filme { get; set; }
        public int FilmeId { get; set; }
        public Genero Genero { get; set; }
        public int GeneroId { get; set; }
    }
}
