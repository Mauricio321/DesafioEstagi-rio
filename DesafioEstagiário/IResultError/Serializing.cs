using FluentResults;
using Servicos.Erros;

namespace DesafioEstagiário.IResultError
{
    public static class ResultExtention
    {
        public static IResult Serialize<T>(Result<T> result)
        {
            if (result.IsFailed)
            {
                if (result.Errors[0] is Forbiden)
                {
                    return TypedResults.Json(result.Errors[0].Message, statusCode: StatusCodes.Status403Forbidden);
                }
                if (result.Errors[0] is BadRequest badRequestError)
                {
                    return TypedResults.Json(new { badRequestError.Message, badRequestError.Failures });
                }

                return TypedResults.Json("Algum erro ocorreu no servidor, ligue para a central", statusCode: StatusCodes.Status500InternalServerError);
            }

            return TypedResults.Ok(result.Value);
        }

        public static IResult Serialize(Result result)
        {
            if (result.IsFailed)
            {
                if (result.Errors[0] is Forbiden)
                {
                    return TypedResults.Json(result.Errors[0].Message, statusCode: StatusCodes.Status403Forbidden);
                }

                if (result.Errors[0] is BadRequest badRequestError)
                {
                    return TypedResults.Json(new { badRequestError.Message, badRequestError.Failures });
                }

                return TypedResults.Json("Algum erro ocorreu no servidor, ligue para a central", statusCode: StatusCodes.Status500InternalServerError);
            }

            return TypedResults.NoContent();
        }
    }
}
