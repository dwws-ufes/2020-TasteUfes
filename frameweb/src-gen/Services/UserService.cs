using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using KissLog;
using FluentValidation;



namespace Services
{
	public class UserService : EntityService<User>, IUserService
	{
		public UserService(IUnitOfWork unitOfWork, INotificator notificator, ILogger logger)
			: base(unitOfWork, notificator, logger) { }
		
		
		
		
			

			
				
			

			public User UpdatePassword(Guid id, string newPassword, string oldPassword)
			{
				throw new NotImplementedException();
			}
		
			

			
				
			

			public User GetByUsername(string username)
			{
				throw new NotImplementedException();
			}
		
			

			
				
			

			public void GetByCredentials(string username, string password)
			{
				throw new NotImplementedException();
			}
		
	}
}
