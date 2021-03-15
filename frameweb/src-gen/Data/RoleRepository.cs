using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



namespace Data
{
	public class RoleRepository : EntityRepository<Role>, IRoleRepository
	{
	    public RoleRepository(ApplicationDbContext context) : base(context) { }
	    
	    
	}
}