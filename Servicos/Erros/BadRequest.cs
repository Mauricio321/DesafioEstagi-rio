using FluentResults;

namespace Servicos.Erros;

public class BadRequest : Error
{
    public List<IEnumerable<ValidationFailure>> Failures { get; }

    public BadRequest(List<IEnumerable<ValidationFailure>> failures) : base("Requisição mal feita")
    {
        Failures = failures;
    }
}

public readonly record struct ValidationFailure(string PropertyName, IEnumerable<string> Errors);
