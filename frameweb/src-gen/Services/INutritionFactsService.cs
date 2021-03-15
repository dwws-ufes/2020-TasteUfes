using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;



namespace Services
{
	public interface INutritionFactsService : IEntityService<NutritionFacts>
	{
		
			

			
				
			

			NutritionFacts Reduce(IEnumerable nutritionFacts);
		
	}
}