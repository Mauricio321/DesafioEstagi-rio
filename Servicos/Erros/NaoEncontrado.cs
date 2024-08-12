using FluentResults;

namespace Servicos.Erros;

public class NaoEncontrado : Error
{
    public NaoEncontrado() : base("Não encontrado") {  }
    public NaoEncontrado(string nomeEntidade) : base($"{nomeEntidade} não foi encontrado/a") 
    {
    }
}
