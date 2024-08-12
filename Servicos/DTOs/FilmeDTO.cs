namespace Servicos.DTOs;

public class FilmeDTO
{
    public string? Nome { get; set; }
    public int FaixaEtaria { get; set; }
    public int Duracao { get; set; }
    public string? Direcao { get; set; }
    public string? AnoDeLancamento { get; set; }
    public string? Roteiristas { get; set; }
    public string? Atores { get; set; }
    public List<int> GeneroId { get; set; }
    public float NotaMedia { get; set; }
}
