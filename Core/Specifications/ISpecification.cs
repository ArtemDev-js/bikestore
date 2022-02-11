using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
			//LINQ Expression of Type Function that take parameter T and return Bool
        Expression<Func<T, bool>> Criteria {get; }
			
			//List of Exp of Type Func param T and return object
				List<Expression<Func<T, object>>> Includes {get; }
    }
}