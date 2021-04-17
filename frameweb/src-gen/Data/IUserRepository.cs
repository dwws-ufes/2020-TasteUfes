using SCAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Data
{
	public interface IUserRepository : IEntityRepository<User>
	{
		
			

			
				
			

			User GetWithRecipes(Guid id);
		
			

			
				
			

			User GetByUsername(string username);
		
	}
}