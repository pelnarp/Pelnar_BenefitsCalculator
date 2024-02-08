using Api.Models;

namespace Api.ErrorHandling
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (Exception ex)
            {
                // Handle other unhandled exceptions
                // Log the exception for debugging purposes.
                Console.WriteLine($"\nUnhandled Exception:\n{ex}\n");

                // Customize the error response as needed.
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new ApiResponse<object> { Success = false, Message = "An error occurred while processing your request." });
            }
        }
    }
}
