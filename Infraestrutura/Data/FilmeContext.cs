using Dominio.Models;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Data
{
    public class FilmeContext : DbContext
    {
        public FilmeContext(DbContextOptions options)
        : base(options) { }

        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<FilmeGenero> FilmeGeneros { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FilmeGenero>().HasKey(lg => new {
                lg.FilmeId,
                lg.GeneroId
            });
        }
    }
}
