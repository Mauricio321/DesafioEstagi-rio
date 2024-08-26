using FluentResults;

namespace Servicos.Erros;

public class BadRequest : Error
{
    public IEnumerable<ValidationFailure> Failures { get; }

    public BadRequest(IEnumerable<ValidationFailure> failures) : base("Requisição mal feita")
    {
        Failures = failures;
    }
}

public readonly record struct ValidationFailure(string PropertyName, IEnumerable<string> Errors);
