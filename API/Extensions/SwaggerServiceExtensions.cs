using Microsoft.OpenApi.Models;

namespace API.Extentions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
             services.AddSwaggerGen(c =>
            {
              c.SwaggerDoc("v1", new OpenApiInfo {Title = "Bikeshop API", Version  = "v1"});
            });

            return services;
        }


        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger(); //creates JSON
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv5 v1")); //creates url on Swagger

            return app;
        }
    }
}