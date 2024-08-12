using FluentResults;

namespace Servicos.Erros;

public class Forbiden : Error
{
    public Forbiden(string mesagemDeErro) : base(mesagemDeErro) 
    {
        if (mesagemDeErro.Length < 5)
            throw new ArgumentException("Mensagem de erro precisa ter pelo menos 5 caracters");
    }
}
