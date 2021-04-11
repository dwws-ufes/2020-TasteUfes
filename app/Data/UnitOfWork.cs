using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage;
using TasteUfes.Models;
using TasteUfes.Data.Interfaces;
using TasteUfes.Data.Context;

namespace TasteUfes.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly Dictionary<string, dynamic> _repositories;

        public IIngredientRepository Ingredients { get; }
        public IFoodRepository Foods { get; }
        public IRecipeRepository Recipes { get; }
        public IPreparationRepository Preparations { get; set; }
        public IPreparationStepRepository PreparationSteps { get; set; }
        public INutritionFactsRepository NutritionFacts { get; }
        public INutritionFactsNutrientsRepository NutritionFactsNutrients { get; }
        public IUserRepository Users { get; }

        public UnitOfWork(ApplicationDbContext context,
            IIngredientRepository ingredients,
            IFoodRepository foods,
            IRecipeRepository recipes,
            IPreparationRepository preparations,
            IPreparationStepRepository preparationSteps,
            INutritionFactsRepository nutritionFacts,
            INutritionFactsNutrientsRepository nutritionFactsNutrients,
            IUserRepository users)
        {
            _context = context;
            _repositories = new Dictionary<string, dynamic>();

            _repositories[nameof(Ingredient)] = Ingredients = ingredients;
            _repositories[nameof(Food)] = Foods = foods;
            _repositories[nameof(Recipe)] = Recipes = recipes;
            _repositories[nameof(Preparation)] = Preparations = preparations;
            _repositories[nameof(PreparationStep)] = PreparationSteps = preparationSteps;
            _repositories[nameof(NutritionFacts)] = NutritionFacts = nutritionFacts;
            _repositories[nameof(NutritionFactsNutrients)] = NutritionFactsNutrients = nutritionFactsNutrients;
            _repositories[nameof(User)] = Users = users;
        }

        public IEntityRepository<TEntity> Repository<TEntity>() where TEntity : Entity
        {
            var type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type))
            {
                return (IEntityRepository<TEntity>)_repositories[type];
            }

            var repositoryType = typeof(EntityRepository<>).MakeGenericType(typeof(TEntity));

            _repositories.Add(type, Activator.CreateInstance(repositoryType, _context));

            return _repositories[type];
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public void DetachEntity<TEntity>(TEntity entity) where TEntity : Entity
        {
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
