using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
	public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>  //basespec type of <Product>
	{
		public ProductsWithTypesAndBrandsSpecification() //empthy constractor to creaste query for all products with Types and Brands
		{
			AddInclude(x => x.ProductType); //AddInclude is a method inside BaseSpec class
			AddInclude(x => x.ProductBrand);
		}

		public ProductsWithTypesAndBrandsSpecification(int id) //constractor to create query for one product with Type and Brand
		: base(x => x.Id == id) 
		{
			AddInclude(x => x.ProductType); //AddInclude is a method inside BaseSpec class
			AddInclude(x => x.ProductBrand);
		}
	}
}