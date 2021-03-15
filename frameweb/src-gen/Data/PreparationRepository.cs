using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



namespace Data
{
	public class PreparationRepository : EntityRepository<Preparation>, IPreparationRepository
	{
	    public PreparationRepository(ApplicationDbContext context) : base(context) { }
	    
	    
	}
}