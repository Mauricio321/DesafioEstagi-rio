using Dominio.Models;

namespace Servicos.Interfaces
{
    public interface IFilmeRepository
    {
        void AdicionarFilmes(Filme filme);
        Task<IEnumerable<Filme>> GetFilmes(int paginas, int quantidadeFilmesPorPagina, List<int> generoIds, string ator, OrdenacaoAvaliacao ordenacaoAvaliacao);
        void DeleteFilme(Filme filme);
        Task<IEnumerable<int>> Avaliacoes();
        Task<int> QuantidadeAvaliacoes();
        void SaveChanges();
        Task<Filme?> FiltrarFilmePorId(int id);
        Task<Filme?> FiltrarFilmePorIdAsync(int id);
        Task SaveChangesAsync();
        void AvaliacaoFilme(Avaliacao avaliacao);
        Task<IEnumerable<Usuario>> GetUsuario(int id);
        Task<Avaliacao?> UsuarioJaAvaliou(int usuarioId, int filmeId);
        void AtualizarAvaliacao(Avaliacao avaliacao);
    }
}
