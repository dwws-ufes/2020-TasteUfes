using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using KissLog;
using FluentValidation;



namespace Services
{
	public class RecipeService : EntityService<Recipe>, IRecipeService
	{
		public RecipeService(IUnitOfWork unitOfWork, INotificator notificator, ILogger logger)
			: base(unitOfWork, notificator, logger) { }
		
		
		
		
			

			
				
			

			public Recipe RecalculateRecipePerServing(Guid id, int servings)
			{
				throw new NotImplementedException();
			}
		
			

			
				
			

			public Recipe CalculateAnonymousRecipe(Recipe recipe)
			{
				throw new NotImplementedException();
			}
		
			

			
				
			

			public IEnumerable<Recipe> RecommendRecipesByIngredients(IEnumerable ingredients)
			{
				throw new NotImplementedException();
			}
		
	}
}
