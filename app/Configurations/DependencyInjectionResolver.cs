using Microsoft.Extensions.DependencyInjection;
using TasteUfes.Data.Interfaces;
using TasteUfes.Data;
using TasteUfes.Data.Context;
using TasteUfes.Models.Validators;
using TasteUfes.Services;
using TasteUfes.Services.Interfaces;
using TasteUfes.Services.Notifications;

namespace TasteUfes.Configurations
{
    public static class DependencyInjectionResolver
    {
        public static IServiceCollection ResolveDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INotificator, Notificator>();

            // User context
            services.AddScoped<UserValidator>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            // Food context
            services.AddScoped<FoodValidator>();
            services.AddScoped<IFoodRepository, FoodRepository>();
            services.AddScoped<IFoodService, FoodService>();

            // NutritionFacts context
            services.AddScoped<INutritionFactsRepository, NutritionFactsRepository>();

            // NutritionFactsNutrients context
            services.AddScoped<INutritionFactsNutrientsRepository, NutritionFactsNutrientsRepository>();

            // Ingredient context
            services.AddScoped<IIngredientRepository, IngredientRepository>();

            return services;
        }
    }
}