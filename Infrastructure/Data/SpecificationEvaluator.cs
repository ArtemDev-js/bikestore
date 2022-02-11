using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity: BaseEntity  //creates a query for us
    {
        public static IQueryable<TEntity> GetQUery(IQueryable<TEntity> inputQuery, //ex. DbSet<Product>
																										ISpecification<TEntity> spec)
				{
							var query = inputQuery;

							if (spec.Criteria != null)
							{
								query = query.Where(spec.Criteria);  //ex: query.Where.(id = ProductId)
							}

							query = spec.Includes.Aggregate(query, (current, include) => current.Include(include)); //Aggregate means combine ex. will include Brands and types

							return query;
				}
    }
}