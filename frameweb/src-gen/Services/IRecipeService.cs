using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;



namespace Services
{
	public interface IRecipeService : IEntityService<Recipe>
	{
		
			

			
				
			

			Recipe RecalculateRecipePerServing(Guid id, int servings);
		
			

			
				
			

			Recipe CalculateAnonymousRecipe(Recipe recipe);
		
			

			
				
			

			IEnumerable<Recipe> RecommendRecipesByFoods(IEnumerable foods);
		
			

			
				
			

			Recipe GetDetailed(Guid id);
		
	}
}