
using Identity.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
            => _logger = logger;

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            int statusCode;
            string title;
            string detail;

            if (exception is NotFoundException)
            {
                statusCode = StatusCodes.Status404NotFound;
                title = "Recurso não encontrado";
                detail = exception.Message;
            }

            else if (exception is RuleBusinessException)
            {
                statusCode = StatusCodes.Status409Conflict;
                title = "Regra de negócio quebrada";
                detail = exception.Message;
            }

            else
            {
                statusCode = StatusCodes.Status500InternalServerError;
                title = "Erro interno no servidor";
                detail = exception.Message;
            }

            if (statusCode >= 500)
            {
                _logger.LogError(exception, "Erro inesperado na API: {Message}", exception.Message);
            }

            ProblemDetails problemDetails = new ProblemDetails()
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Instance = httpContext.Request.Path
            };

            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}