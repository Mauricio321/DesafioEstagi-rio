using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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
        public DbSet<Administrador> Administradores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FilmeGenero>().HasKey(lg => new
            {
                lg.FilmeId,
                lg.GeneroId
            });

            modelBuilder.Entity<Role>()
            .HasData(new Role
            {
                Id = 1,
                Name = "manager"

            },
            new Role
            {
                Id = 2,
                Name = "authenticated"
            });

            modelBuilder.Entity<Usuario>()
                            .HasData(new Usuario { Id = 1, Email = "adminlivraria@gmail.com", Senha = "admin123@",Nome = "Admin" , RoleId = 1 });
        }
    }
}
