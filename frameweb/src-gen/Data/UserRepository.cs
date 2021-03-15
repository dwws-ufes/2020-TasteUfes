using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



namespace Data
{
	public class UserRepository : EntityRepository<User>, IUserRepository
	{
	    public UserRepository(ApplicationDbContext context) : base(context) { }
	    
	    
			

			
				
			

			public User GetWithRecipes(Guid id)
			{
				throw new NotImplementedException();
			}
		
	}
}