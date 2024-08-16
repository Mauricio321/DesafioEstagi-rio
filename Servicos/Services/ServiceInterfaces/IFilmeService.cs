using Dominio.Models;
using FluentResults;
using Servicos.DTOs;

namespace Servicos.Services.ServiceInterfaces
{
    public interface IFilmeService
    {
        Task<Result> AdicionarFilmes(FilmeDTO filmeDTO);
        Task<ListaDeFilmesDto> GetFilmes(int paginas, int quantidadeFilmesPorPagina, List<int> generoIds, string ator, OrdenacaoAvaliacao ordenacaoAvaliacao);
        Task DeleteFilmeAsync(int id);
        Task<Avaliacao> AvaliacaoFilme(AvaliacaoDTO avaliacao, int id);
        Task<List<Avaliacao>> GetAvaliacoes(int id);
    }
}
