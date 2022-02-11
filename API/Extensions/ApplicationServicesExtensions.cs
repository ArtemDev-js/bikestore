using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Extentions
{
    public static class ApplicationServicesExtensions //we extend services here and then add them back to the Startup.cs
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            
            services.AddScoped<IProductRepository, ProductRepository>(); //<interface, and instance of the related class>
			services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));

            //service for getting error messeges from ModelState
            services.Configure<ApiBehaviorOptions>(options => 
            {
              options.InvalidModelStateResponseFactory = actionContext => 
              {
                var errors = actionContext.ModelState
                  .Where(e => e.Value.Errors.Count > 0)
                  .SelectMany(x => x.Value.Errors)
                  .Select(x => x.ErrorMessage).ToArray();

                  var errorResponse = new ApiValidationErrorResponse
                  {
                    Errors = errors
                  };

                  return new BadRequestObjectResult(errorResponse);
              };
            });

            return services;
        }
    }
}