using System.Net;
using System.Text.Json;
using API.Errors;
using Microsoft.Extensions.Hosting;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next; // RequestDelegate is a task that represents the completion of HTTP request and return the complition result
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env; //checks if we are in dev enviroment
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,

        IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); //if the request is Ok it moves to the next stage(Middleware) in a pipeline in Startup.cs
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message); //log an error to the console
                context.Response.ContentType = "application/json";  //will save the response to the json file for our 'client'
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                //if we are in development mode ? do this : otherwise do this (prod)
                var response = _env.IsDevelopment() 
                                ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                                : new ApiException((int)HttpStatusCode.InternalServerError); //less details in prod mode

                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

                var json = JsonSerializer.Serialize(response, options); //format response to CamlCase(var options) json text


                await context.Response.WriteAsync(json);
            }
        }
    }
}