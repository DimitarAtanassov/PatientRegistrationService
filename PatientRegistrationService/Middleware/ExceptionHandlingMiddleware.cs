using System.Net;
using System.Text.Json;
using PatientRegistrationService.Models;
using Serilog;

namespace PatientRegistrationService.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IHostEnvironment env)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Move to the next middleware in the pipeline
                await next(context);
            }
            catch (Exception ex)
            {
                // Log
                logger.LogError(ex, ex.Message);

                // Set the content type and status code 
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
 
                // Create the error response based on the environment (dev or prod)
                var response = env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new ApiException(context.Response.StatusCode, ex.Message, "Internal Server Error");

                // Serialize the response to JSON
                var json = JsonSerializer.Serialize(response);

                // Write the JSON response to the response body
                await context.Response.WriteAsync(json);
            }
        }
    }
}
