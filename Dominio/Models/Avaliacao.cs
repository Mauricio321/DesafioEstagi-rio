namespace Dominio.Models
{
    public class Avaliacao
    {
        public int Id { get; set; }
        public int Nota { get; set; }
        public int FilmeId { get; set; }
        public Filme Filme { get; set; } = default!;
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = default!;
        public string? Comentario { get; set; }
    }
}

