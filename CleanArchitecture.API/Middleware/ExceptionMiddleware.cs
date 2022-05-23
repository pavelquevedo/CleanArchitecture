using CleanArchitecture.API.Errors;
using CleanArchitecture.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace CleanArchitecture.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";

                var statusCode = (int)HttpStatusCode.InternalServerError;
                var result = string.Empty;
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                switch (ex)
                {
                    case NotFoundException notFoundException:
                        statusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case ValidationException validationException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        var validationJson = JsonSerializer.Serialize(validationException.Errors, options);
                        result = JsonSerializer.Serialize(new CodeErrorException(statusCode, ex.Message, validationJson), options);
                        break;

                    case BadRequestException badRequestException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    default:
                        break;
                }

                if (string.IsNullOrEmpty(result))
                {
                    result = JsonSerializer.Serialize(new CodeErrorException(statusCode, ex.Message, ex.StackTrace), options);
                }

                var response = _environment.IsDevelopment()
                    ? result
                    : JsonSerializer.Serialize(new CodeErrorException((int)HttpStatusCode.InternalServerError), options);

                //var json = JsonSerializer.Serialize(response, options);
                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsync(response);
            }
        }
    }
}
