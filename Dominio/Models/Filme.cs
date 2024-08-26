using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Models
{
    public class Filme
    {
        public required string Nome { get; set; }
        public required int FaixaEtaria { get; set; }
        public required int Duracao { get; set; }
        public required string Direcao { get; set; }
        public required string AnoDeLancamento { get; set; }
        public required string Roteiristas { get; set; }
        public required string Atores { get; set; }
        public int FilmeId { get; set; }
        public IEnumerable<FilmeGenero> Generos { get; set; } = new List<FilmeGenero>();
        public IEnumerable<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();
        public int NotaMedia => GetNotaMedia();

        private int GetNotaMedia()
        {
            var quanttidadeAvaliacoes = Avaliacoes.Count();
            if (quanttidadeAvaliacoes == 0)
            {
                return 0;
            }

            return Avaliacoes.Sum(n => n.Nota) / quanttidadeAvaliacoes;
        }

    }

    public enum OrdenacaoAvaliacao
    {
        MenorParaMaior = 1,
        MaiorParaMenor = 2
    }
}