using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{

	//DbContext(StoreContext) will represent a session with a DB.
	public class StoreContext : DbContext
	{

		//DbContextOptions will help us to configure ConnectionString to query data from DB
		public StoreContext(DbContextOptions <StoreContext> options) : base(options)
		{

		}

		//DbSet property will allow to query the data from DB by using methods awailable in DbContext (ex. "Find" etc.)

		public DbSet <Product> Products { get; set; } //Products is a table name in DB that will be generated when the code will executed

		public DbSet <ProductBrand> ProductBrands {get; set;}
		
		public DbSet <ProductType> ProductTypes {get; set;}

		

		//OnMOdelCreating method that is resp. for creating migrations
		protected override void OnModelCreating(ModelBuilder modelBuilder)  
		{
			base.OnModelCreating(modelBuilder);  //base is DbContext class
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
		
		
	}
}