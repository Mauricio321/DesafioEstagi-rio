using Dominio.Models;
using Servicos.DTOs;

namespace Servicos.Services.ServiceInterfaces
{
    public interface IFilmeService
    {
        string AdicionarFilmes(FilmeDTO filmeDTO);
        ListaDeFilmes GetFilmes(int paginas, int quantidadeFilmesPorPagina, List<int> generoIds, string ator, OrdenacaoAvaliacao ordenacaoAvaliacao);
        void DeleteFilme(Filme filme);
    }
}
