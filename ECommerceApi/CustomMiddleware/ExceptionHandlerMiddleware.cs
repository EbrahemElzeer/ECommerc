using ECommerce.Service.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.CustomMiddleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next ,ILogger<ExceptionHandlerMiddleware> logger)
        {
             _next = next;
            _logger = logger;
        }
       
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);

                if (context.Response.StatusCode== StatusCodes.Status404NotFound)
                {
                    var problem = new ProblemDetails()
                    {

                        Title = "The resource you are looking for was not found.",
                        Status = StatusCodes.Status404NotFound,
                        Instance = context.Request.Path,
                        Detail = $"The requested URL {context.Request.Path} was not found on this server."

                    };
                    await context.Response.WriteAsJsonAsync(problem);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Somethimg Went wrong");
               //context.Response.StatusCode=StatusCodes.Status500InternalServerError;
                var problem = new ProblemDetails()
                {
                    Title = "An error occurred while processing your request.",
                    Instance= context.Request.Path,
                    Detail = ex.Message,
                    Status = ex switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status500InternalServerError

                    },

                };
               context.Response.StatusCode=problem.Status.Value;
                await context.Response.WriteAsJsonAsync(problem);

            }
        }
    }
}
