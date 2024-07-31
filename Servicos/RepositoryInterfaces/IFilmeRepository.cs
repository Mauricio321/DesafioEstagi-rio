﻿using Dominio.Models;

namespace Servicos.Interfaces
{
    public interface IFilmeRepository
    {
        void AdicionarFilmes(Filme filme);
        IEnumerable<Filme> GetFilmes(int paginas, int quantidadeFilmesPorPagina, List<int> generoIds, string ator, OrdenacaoAvaliacao ordenacaoAvaliacao);
        void DeleteFilme(Filme filme);
        IEnumerable<Filme> GetFilmess();
        IEnumerable<int> Avaliacoes();
        int QuantidadeAvaliacoes();
        void SaveChanges();
        Filme? FiltrarFilmePorId(int id);
        Task<Filme?> FiltrarFilmePorIdAsync(int id);
        Task SaveChangesAsync();
        void AvaliacaoFilme(Avaliacao avaliacao);
    }
}
