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
    public class RecipesController : EntityApiControllerV1<Recipe, RecipeResource>
    {
        public RecipesController(IRecipeService recipeService, IMapper mapper, INotificator notificator)
            : base(recipeService, mapper, notificator) { }
    }
}
