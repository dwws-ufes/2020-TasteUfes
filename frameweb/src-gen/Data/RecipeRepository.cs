using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



namespace Data
{
	public class RecipeRepository : EntityRepository<Recipe>, IRecipeRepository
	{
	    public RecipeRepository(ApplicationDbContext context) : base(context) { }
	    
	    
			

			
				
			

			public Recipe GetDetailed(Guid id)
			{
				throw new NotImplementedException();
			}
		
	}
}