using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args) //.net run will start here in Main class
        {
            var host = CreateHostBuilder(args).Build();

						using (var scope = host.Services.CreateScope())  //everything will be desposed after executing inside Using block (it was used here because this service is outside Startup class where all the services are sitting)
						{
								var services = scope.ServiceProvider;
								var loggerFactory = services.GetRequiredService<ILoggerFactory>();

								try
								{
									//1. context is an instance of StoreContext service
									var context = services.GetRequiredService<StoreContext>();
									//2. MigrateAsync Asynchronously applies any pending migrations for the context to the database. Will create the database if it does not already exist.
									await context.Database.MigrateAsync();  
									//3. StoreContextSeed method SeedAsync to add data to DB if needed.
									await StoreContextSeed.SeedAsync(context, loggerFactory);
								}
								catch (Exception ex)
								{
									var logger = loggerFactory.CreateLogger<Program>();
									logger.LogError(ex, "An error occured during migration");
								}
						}

						host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>(); //Startup !!!
                });
    }
}
