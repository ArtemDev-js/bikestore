using API.Extentions;
using API.Helpers;
using API.Middleware;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Startup
    {
    private readonly IConfiguration _config;

        public Startup(IConfiguration config)  //IConfiguration class was injected in Startup class DI
        {
      			_config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the Dependancy Injection container.
        public void ConfigureServices(IServiceCollection services) //IServiceCollection class was injected in Startup class DI
        {
						//service lifetime: Scoped - the service will be alived for the lifetime of a HTTP request


						services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers(); 
						services.AddSwaggerGen();
						services.AddDbContext<StoreContext>(x => x.UseSqlite(_config.GetConnectionString("DefaultConnection")));

            services.AddApplicationServices(); //add services from ourcd ApplicationServicesExtensions.cs
            services.AddSwaggerDocumentation();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
				// **Middleware*** it's a software that will manipuilate/do something with the HTTP request in the way in or out from API Controller
				//the ordering is important !!!
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>(); //first middleware !!!

            app.UseStatusCodePagesWithReExecute("/errors/{0}"); // when we will hit this middlware it will go to our ErrorController with a statusCode {}

            app.UseHttpsRedirection(); //if we accidantly went to HTTP it will autoredirect us to HTTPS.

            app.UseRouting(); //to be able to access API controller and it's Endpoints via middleware

						app.UseStaticFiles(); //this middleware allowes to use Images for example (wwwroot)

            app.UseAuthorization();

            app.UseSwaggerDocumentation();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
