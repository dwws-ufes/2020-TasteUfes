using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



namespace Data
{
	public class NutrientRepository : EntityRepository<Nutrient>, INutrientRepository
	{
	    public NutrientRepository(ApplicationDbContext context) : base(context) { }
	    
	    
	}
}