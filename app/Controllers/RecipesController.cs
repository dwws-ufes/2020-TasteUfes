using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasteUfes.Controllers.Contracts.Requests;
using TasteUfes.Controllers.Contracts.Responses;
using TasteUfes.Models;
using TasteUfes.Services.Interfaces;
using TasteUfes.Services.Notifications;

namespace TasteUfes.Controllers
{
    [Authorize]
    public class RecipesController : EntityApiControllerV1<Recipe, RecipeRequest, RecipeResponse>
    {
        private readonly IRecipeService _recipeService;
        private readonly IUserService _userService;

        public RecipesController(IUserService userService,
            IRecipeService recipeService, IMapper mapper, INotificator notificator)
            : base(recipeService, mapper, notificator)
        {
            _recipeService = recipeService;
            _userService = userService;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public override ActionResult<RecipeResponse> Get([FromRoute] Guid id)
        {
            var recipe = _recipeService.GetDetailed(id);

            if (HasErrors())
                return BadRequest(Errors());

            if (recipe == null)
                return NotFound();

            return Mapper.Map<RecipeResponse>(recipe);
        }

        [HttpGet]
        [AllowAnonymous]
        public override ActionResult<IEnumerable<RecipeResponse>> Get() => base.Get();

        [HttpPost]
        public override ActionResult<RecipeResponse> Post([FromBody] RecipeRequest resource)
        {
            ModelState.Remove("UserId");

            if (!ModelState.IsValid)
                return BadRequest(Errors(resource));

            var user = _userService.GetByUsername(User.Identity.Name);

            if (user == null)
                return Forbid();

            resource.UserId = user.Id;

            return base.Post(resource);
        }

        [HttpPut("{id}")]
        public override ActionResult<RecipeResponse> Put([FromRoute] Guid id, [FromBody] RecipeRequest resource)
        {
            ModelState.Remove("User");
            ModelState.Remove("UserId");
            ModelState.Remove("NutritionFacts");

            if (!ModelState.IsValid)
                return BadRequest(Errors(resource));

            if (id != resource?.Id)
                return NotFound();

            var recipe = _recipeService.Get(id);

            if (HasErrors() || recipe == null)
                return NotFound();

            var user = _userService.GetByUsername(User.Identity.Name);

            if (recipe.UserId != user.Id && !User.IsInRole("Admin"))
                return Forbid();

            /**
             * Prevenção de atualização aninhada.
             */
            resource.User = null;
            resource.NutritionFacts = null;
            resource.Ingredients = resource.Ingredients.Select(i => { i.Food = null; return i; });

            return base.Put(id, resource);
        }

        [HttpDelete("{id}")]
        public override IActionResult Delete([FromRoute] Guid id)
        {
            var recipe = _recipeService.Get(id);

            if (HasErrors())
                return NotFound();

            var user = _userService.GetByUsername(User.Identity.Name);

            if (HasErrors())
                return NotFound();

            // Checa se o usuário logado e o requisitado são os mesmos.
            // A regra não se aplica a administradores.
            if (user.Id != recipe.UserId && !User.IsInRole("Admin"))
                return Forbid();

            return base.Delete(id);
        }

        [HttpGet("{id}/recalculate-per-servings/{servings}")]
        [AllowAnonymous]
        public ActionResult<RecipeResponse> RecalculateRecipePerServing(Guid id, int servings)
        {
            if (!_recipeService.Exists(id))
                return NotFound();

            var recipe = _recipeService.RecalculateRecipePerServings(id, servings);

            if (HasErrors())
                return NotFound(Errors());

            return Ok(Mapper.Map<RecipeResponse>(recipe));
        }

        [HttpPost("calculate-anonymous")]
        [AllowAnonymous]
        public ActionResult<AnonymousRecipeResponse> CalculateAnonymousRecipe(AnonymousRecipeRequest resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(Errors(resource));

            var recipe = Mapper.Map<Recipe>(resource);

            recipe.Servings = 1;

            var anonymous = _recipeService.CalculateAnonymousRecipe(recipe);

            if (HasErrors())
                return BadRequest(Errors(resource));

            return Ok(Mapper.Map<AnonymousRecipeResponse>(anonymous));
        }

        [HttpPost("recommend-by-foods")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<RecipeResponse>> RecommendRecipesByFoods(IEnumerable<FoodRequest> resource)
        {
            foreach (var name in ModelState.Keys.Where(k => k.EndsWith("Name")))
            {
                ModelState.Remove(name);
            }

            if (!ModelState.IsValid)
                return BadRequest(Errors(resource));

            var recipes = _recipeService.RecommendRecipesByFoods(Mapper.Map<IEnumerable<Food>>(resource));

            if (HasErrors())
                return BadRequest(Errors(resource));

            return Ok(Mapper.Map<IEnumerable<RecipeResponse>>(recipes));
        }
    }
}
