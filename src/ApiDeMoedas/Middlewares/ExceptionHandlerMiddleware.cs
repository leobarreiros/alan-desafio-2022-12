using System.Text.Json;

namespace ApiDeMoedas.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var tag = exception.Source;
            var descricao = exception.Message;

            if (exception.StackTrace != null)
                descricao += $" {exception.StackTrace}";

            var exceptionResult = JsonSerializer.Serialize(new[]
            {
                new
                {
                    descricao,
                    tag
                }
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            return context.Response.WriteAsync(exceptionResult);
        }
    }
}
