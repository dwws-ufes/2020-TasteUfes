using System;
using System.Collections.Generic;
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
            return Mapper.Map<RecipeResource>(_recipeService.GetDetailed(id));
        }

        [HttpGet]
        [AllowAnonymous]
        public override ActionResult<IEnumerable<RecipeResource>> Get()
        {
            return base.Get();
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
    }
}
