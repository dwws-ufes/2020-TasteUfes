using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



namespace Data
{
	public class NutritionFactsNutrientsRepository : EntityRepository<NutritionFactsNutrients>, INutritionFactsNutrientsRepository
	{
	    public NutritionFactsNutrientsRepository(ApplicationDbContext context) : base(context) { }
	    
	    
	}
}