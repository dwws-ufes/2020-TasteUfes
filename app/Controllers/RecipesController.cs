using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasteUfes.Models;
using TasteUfes.Resources;
using TasteUfes.Services.Interfaces;
using TasteUfes.Services.Notifications;

namespace TasteUfes.Controllers
{
    [Authorize]
    public class RecipesController : EntityApiControllerV1<Recipe, RecipeResource>
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
        public override ActionResult<RecipeResource> Get([FromRoute] Guid id)
        {
            var recipe = _recipeService.GetDetailed(id);

            if (HasErrors())
                return BadRequest(Errors());

            if (recipe == null)
                return NotFound();

            return Mapper.Map<RecipeResource>(recipe);
        }

        [HttpGet]
        [AllowAnonymous]
        public override ActionResult<IEnumerable<RecipeResource>> Get() => base.Get();

        [HttpPost]
        public override ActionResult<RecipeResource> Post([FromBody] RecipeResource resource)
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
        public override ActionResult<RecipeResource> Put([FromRoute] Guid id, [FromBody] RecipeResource resource)
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
        public ActionResult<RecipeResource> RecalculateRecipePerServing(Guid id, int servings)
        {
            if (!_recipeService.Exists(id))
                return NotFound();

            var recipe = _recipeService.RecalculateRecipePerServings(id, servings);

            if (HasErrors())
                return NotFound(Errors());

            return Ok(Mapper.Map<RecipeResource>(recipe));
        }

        [HttpPost("calculate-anonymous")]
        [AllowAnonymous]
        public ActionResult<AnonymousRecipeResource> CalculateAnonymousRecipe(AnonymousRecipeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(Errors(resource));

            var anonymous = _recipeService.CalculateAnonymousRecipe(Mapper.Map<Recipe>(resource));

            if (HasErrors())
                return BadRequest(Errors(resource));

            return Ok(Mapper.Map<AnonymousRecipeResource>(anonymous));
        }

        [HttpPost("recommend-by-ingredients")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<RecipeResource>> RecommendRecipesByIngredients(IEnumerable<FoodResource> resource)
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

            return Ok(Mapper.Map<IEnumerable<RecipeResource>>(recipes));
        }
    }
}
