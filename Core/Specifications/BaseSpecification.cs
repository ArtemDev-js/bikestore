using System.Linq.Expressions;

namespace Core.Specifications
{
	public class BaseSpecification<T> : ISpecification<T>
	{
		public BaseSpecification()
		{
			
		}

		public BaseSpecification(Expression<Func<T, bool>> criteria)
		{
			Criteria = criteria;
		}

		public Expression<Func<T, bool>> Criteria  {get; }

		public List<Expression<Func<T, object>>> Includes {get; } = new List<Expression<Func<T, object>>>(); // 'Includes' method by default it set to empty List

		protected void AddInclude(Expression<Func<T, object>> includeExpression) //protected is accesible in this class and child (derived) classes
		{
			Includes.Add(includeExpression);
		}
	}
}