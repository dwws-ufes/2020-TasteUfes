using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



namespace Data
{
	public class IngredientRepository : EntityRepository<Ingredient>, IIngredientRepository
	{
	    public IngredientRepository(ApplicationDbContext context) : base(context) { }
	    
	    
	}
}