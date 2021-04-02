using System;
using System.Collections.Generic;
using TasteUfes.Models;

namespace TasteUfes.Services.Interfaces
{
    public interface IRecipeService : IEntityService<Recipe>
    {
        Recipe GetDetailed(Guid id);
        Recipe RecalculateRecipePerService(Guid id, int servings);
        Recipe CalculateAnonymousRecipe(Recipe recipe);
        IEnumerable<Recipe> RecommendRecipesByIngredients(IEnumerable<Ingredient> ingredients);
    }
}