using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



namespace Data
{
	public class PreparationStepRepository : EntityRepository<PreparationStep>, IPreparationStepRepository
	{
	    public PreparationStepRepository(ApplicationDbContext context) : base(context) { }
	    
	    
	}
}