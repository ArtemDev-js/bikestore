using System.Text.Json;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                //brands
                if (!context.ProductBrands.Any()) //if !NO Any ProductBrands in our DB(context) add them from a JSON file...
                {
                    var brandsData = File.ReadAllText(
                        "../Infrastructure/Data/SeedData/brands.json"
                    ); //the path to file to be read

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData); //JsonSerializer.Deserialize convert Text to Objects

                    foreach (var item in brands) //Loop brands and add to the DB table productBrands
                    {
                        context.ProductBrands.Add(item);
                    }

                    await context.SaveChangesAsync(); //save changes in DB at the end
                }

                //types
                if (!context.ProductTypes.Any()) //if !NO Any ProductBrands in our DB(context) add them from a JSON file...
                {
                    var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json"); //the path to file to be read

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData); //JsonSerializer.Deserialize convert Text to Objects

                    foreach (var item in types) //Loop brands and add to the DB table productBrands
                    {
                        context.ProductTypes.Add(item);
                    }

                    await context.SaveChangesAsync(); //save changes in DB at the end
                }

                //products
                if (!context.Products.Any()) //if !NO Any ProductBrands in our DB(context) add them from a JSON file...
                {
                    var productsData = File.ReadAllText(
                        "../Infrastructure/Data/SeedData/products.json"
                    ); //the path to file to be read

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData); //JsonSerializer.Deserialize convert Text to Objects

                    foreach (var item in products) //Loop brands and add to the DB table productBrands
                    {
                        context.Products.Add(item);
                    }

                    await context.SaveChangesAsync(); //save changes in DB at the end
                }
            }
            catch (Exception ex)
						{ 
							var logger = loggerFactory.CreateLogger<StoreContextSeed>(); //create a logger for this class
							logger.LogError(ex.Message);
						}
        }
    }
}
