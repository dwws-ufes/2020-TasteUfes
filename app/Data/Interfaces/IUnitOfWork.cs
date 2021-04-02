using System;
using TasteUfes.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace TasteUfes.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IIngredientRepository Ingredients { get; }
        IFoodRepository Foods { get; }
        IRecipeRepository Recipes { get; }
        IPreparationRepository Preparations { get; set; }
        IPreparationStepRepository PreparationSteps { get; set; }
        INutritionFactsRepository NutritionFacts { get; }
        INutritionFactsNutrientsRepository NutritionFactsNutrients { get; }
        IUserRepository Users { get; }

        int SaveChanges();
        IDbContextTransaction BeginTransaction();
        IEntityRepository<TEntity> Repository<TEntity>() where TEntity : Entity;
    }
}
