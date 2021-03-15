using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;



namespace TasteUfes.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : BaseController
	{
		
			private readonly IUserService _userService;
		
		
		public UserController(IUserService userService, IMapper mapper, INotificator notificator)
			: base(mapper, notificator)
		{
			
				_userService = userService;
			
		}
		
		
			

			
				[HttpPost]
			
			public IActionResult Post(User user)
			{
				throw new NotImplementedException();
			}
		
			

			
				[HttpPut]
			
			public IActionResult Put(Guid id, User user)
			{
				throw new NotImplementedException();
			}
		
			

			
				[HttpGet]
			
			public IActionResult Get()
			{
				throw new NotImplementedException();
			}
		
			

			
				[HttpGet]
			
			public IActionResult Get(Guid id)
			{
				throw new NotImplementedException();
			}
		
			

			
				[HttpGet]
			
			public IActionResult Authenticate(string email, string password)
			{
				throw new NotImplementedException();
			}
		
			

			
				[HttpGet]
			
			public IActionResult Logout()
			{
				throw new NotImplementedException();
			}
		
			

			
				[HttpDelete]
			
			public IActionResult Delete(Guid id)
			{
				throw new NotImplementedException();
			}
		
	}
}