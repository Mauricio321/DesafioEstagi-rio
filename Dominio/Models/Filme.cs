using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Models
{
    public class Filme
    {
        public string? Nome { get; set; }
        public int FaixaEtaria { get; set; }
        public int Duracao { get; set; }
        public string? Direcao { get; set; }
        public string? AnoDeLancamento { get; set; }
        public string? Roteiristas { get; set; }
        public string? Atores { get; set; }
        public int FilmeId { get; set; }
        public IList<FilmeGenero> Generos { get; set; } = new List<FilmeGenero>();
        public IList<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();
        public int NotaMedia => Avaliacoes.Sum(n => n.Nota) / Avaliacoes.Count();
    }

    public enum OrdenacaoAvaliacao 
    {
        MenorParaMaior = 1,
        MaiorParaMenor = 2
    }
}