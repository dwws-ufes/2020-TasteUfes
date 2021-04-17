using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;



namespace Services
{
	public interface IUserService : IEntityService<User>
	{
		
			

			
				
			

			User UpdatePassword(Guid id, string newPassword, string oldPassword);
		
			

			
				
			

			User GetByUsername(string username);
		
			

			
				
			

			void GetByCredentials(string username, string password);
		
	}
}