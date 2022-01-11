using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
    private readonly IConfiguration _config;

        public Startup(IConfiguration config)  //IConfiguration class was injected in Startup class DI
        {
      			_config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
				
        public void ConfigureServices(IServiceCollection services) //IServiceCollection class was injected in Startup class DI
        {
						//The ordering doesnt metter all of them will start
						//service lifetime: Scoped - the service will be alived for the lifetime of a HTTP request

						services.AddScoped<IProductRepository, ProductRepository>(); //<interface, an instance of the related class>
            services.AddControllers();
						services.AddSwaggerGen();
						services.AddDbContext<StoreContext>(x => x.UseSqlite(_config.GetConnectionString("DefaultConnection")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
				// **Middleware*** it's a software that will manipuilate/do something with the HTTP request in the way in or out from API Controller
				//the ordering is important !!!
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); //if we are in developement mode this action will give us more info about the exceptuion if occure
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv5 v1"));
            }

            app.UseHttpsRedirection(); //if we accidantly went to HTTP it will autoredirect us to HTTPS.

            app.UseRouting(); //to be able to access API controller and it's Endpoints via middleware

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
