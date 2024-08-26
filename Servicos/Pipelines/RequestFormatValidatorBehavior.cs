using FluentResults;
using FluentValidation;
using MediatR;
using Servicos.Erros;
using System.ComponentModel.DataAnnotations;

namespace Servicos.Pipelines;

public class RequestFormatValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : ResultBase<TResponse>, new()
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> validator;
    public RequestFormatValidatorBehavior(IEnumerable<IValidator<TRequest>> validator)
    {
        this.validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validator.Any()) return await next();

        var l = new List<FluentValidation.Results.ValidationFailure>();
        foreach (var val in validator)
        {
            var results = await val.ValidateAsync(request, cancellationToken);

            if (!results.IsValid)
            {
                l.AddRange(results.Errors);
            }
        }
        if (l.Any())
        {
            var validationFailures = l.GroupBy(n => n.PropertyName).Select(f => new ValidationFailure(f.Key, f.Select(e => e.ErrorMessage)));

            return new TResponse().WithError(new BadRequest(validationFailures));
        }
        return await next();
    }
}
