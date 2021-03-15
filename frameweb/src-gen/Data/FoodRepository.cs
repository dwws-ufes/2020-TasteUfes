using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



namespace Data
{
	public class FoodRepository : EntityRepository<Food>, IFoodRepository
	{
	    public FoodRepository(ApplicationDbContext context) : base(context) { }
	    
	    
			

			
				
			

			public Food GetWithNutritionFacts(Guid id)
			{
				throw new NotImplementedException();
			}
		
	}
}