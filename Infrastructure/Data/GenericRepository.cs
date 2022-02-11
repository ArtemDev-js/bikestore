using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

		public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

				public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
				{
						return await ApplySpecification(spec).FirstOrDefaultAsync(); //ApplySpes is creating IQueryable, FirstOrDefaultAsync is a method in StoreContext(DBContext) to run query agaist the DB
				}

				public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
				{
						return await ApplySpecification(spec).ToListAsync();
				}

				private IQueryable<T> ApplySpecification(ISpecification<T> spec) //this method returns query from SpecificationEvaluator and convert it to IQueryable object
				{
					return SpecificationEvaluator<T>.GetQUery(_context.Set<T>().AsQueryable(), spec);
				}
	}
}
