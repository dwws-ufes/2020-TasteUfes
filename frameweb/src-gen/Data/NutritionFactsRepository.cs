using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



namespace Data
{
	public class NutritionFactsRepository : EntityRepository<NutritionFacts>, INutritionFactsRepository
	{
	    public NutritionFactsRepository(ApplicationDbContext context) : base(context) { }
	    
	    
	}
}