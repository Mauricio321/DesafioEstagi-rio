using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Servicos.Pipelines;

public class ErrorHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : ResultBase<TResponse>, new()
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<ErrorHandlingBehavior<TRequest, TResponse>> logger;

    public ErrorHandlingBehavior(ILogger<ErrorHandlingBehavior<TRequest, TResponse>> logger)
    {
        this.logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: ex.Message);

            return new TResponse().WithError("Erro desconhecido");
        }
    }
}
