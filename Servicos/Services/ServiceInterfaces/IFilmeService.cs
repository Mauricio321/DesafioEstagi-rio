using Dominio.Models;
using Servicos.DTOs;

namespace Servicos.Services.ServiceInterfaces
{
    public interface IFilmeService
    {
        string AdicionarFilmes(FilmeDTO filmeDTO);
        ListaDeFilmes GetFilmes(int paginas, int quantidadeFilmesPorPagina, List<int> generoIds, string ator, OrdenacaoAvaliacao ordenacaoAvaliacao);
        void DeleteFilme(int id);
        Task DeleteFilmeAsync(int id);
        Avaliacao AvaliacaoFilme(AvaliacaoDTO avaliacao);
        List<Avaliacao> GetAvaliacoes(int id);
    }
}
