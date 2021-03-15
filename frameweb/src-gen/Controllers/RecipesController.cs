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
	public class RecipesController : BaseController
	{
		
			private readonly IRecipeService _recipeService;
		
			private readonly IFoodService _foodService;
		
		
		public RecipesController(IRecipeService recipeService, IFoodService foodService, IMapper mapper, INotificator notificator)
			: base(mapper, notificator)
		{
			
				_recipeService = recipeService;
			
				_foodService = foodService;
			
		}
		
		
			

			
				[HttpPost]
			
			public IActionResult Post(Recipe recipe)
			{
				throw new NotImplementedException();
			}
		
			

			
				[HttpPut]
			
			public IActionResult Put(Guid id, Recipe recipe)
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
		
			

			
				[HttpDelete]
			
			public IActionResult Delete(Guid id)
			{
				throw new NotImplementedException();
			}
		
			

			
			public IActionResult GetRecipesByUser(Guid user_id)
			{
				throw new NotImplementedException();
			}
		
			

			
				[HttpGet]
			
			public IActionResult RecalculateRecipePerServing(Guid id, int servings)
			{
				throw new NotImplementedException();
			}
		
			

			
				[HttpGet]
			
			public IActionResult CalculateAnonymousRecipe(Recipe recipe)
			{
				throw new NotImplementedException();
			}
		
			

			
				[HttpGet]
			
			public IActionResult RecommendRecipesByIngredients(IEnumerable ingredients)
			{
				throw new NotImplementedException();
			}
		
	}
}