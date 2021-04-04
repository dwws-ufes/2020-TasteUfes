using System;
using System.Collections.Generic;
using TasteUfes.Models;

namespace TasteUfes.Services.Interfaces
{
    public interface IRecipeService : IEntityService<Recipe>
    {
        Recipe GetDetailed(Guid id);
        Recipe RecalculateRecipePerServings(Guid id, int servings);
        Recipe CalculateAnonymousRecipe(Recipe recipe);
        IEnumerable<Recipe> RecommendRecipesByIngredients(IEnumerable<Ingredient> ingredients);
    }
}