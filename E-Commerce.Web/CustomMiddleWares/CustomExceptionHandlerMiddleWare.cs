using DomainLayer.Exceptions;
using Shared.ErrorModels;

namespace E_Commerce.Web.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleWare> logger)
        {
            _next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
                await HandleNotFoundEndPointAsync(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Something Went Wrong");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            var response = new ErrorToReturn
            {
                ErrorMessage = ex.Message
            };

            response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                BadRequestException badRequestException => GetBadRequestError(badRequestException, response),
                _ => StatusCodes.Status500InternalServerError
            };

            httpContext.Response.StatusCode = response.StatusCode;

            await httpContext.Response.WriteAsJsonAsync(response);
        }

        private static int GetBadRequestError(BadRequestException badRequestException, ErrorToReturn response)
        {
            response.Errors = badRequestException.Errors;
            return StatusCodes.Status400BadRequest;
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var response = new ErrorToReturn
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"End Point {httpContext.Request.Path} is Not Found"
                };
                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
