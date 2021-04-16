using Microsoft.Extensions.DependencyInjection;
using TasteUfes.Data.Interfaces;
using TasteUfes.Data;
using TasteUfes.Data.Context;
using TasteUfes.Models.Validators;
using TasteUfes.Services;
using TasteUfes.Services.Interfaces;
using TasteUfes.Services.Notifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace TasteUfes.Configurations
{
    public static class DependencyInjectionResolver
    {
        public static IServiceCollection ResolveDependencyInjections(this IServiceCollection services, IConfiguration configuration)
        {
            // Essentials
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INotificator, Notificator>();

            // Jwt settings
            services.Configure<JwtSettings>(settings =>
            {
                settings.SecretKey = configuration["SECRET_KEY"];
                settings.RefreshLifetime = TimeSpan.Parse(configuration["REFRESH_LIFETIME"]);
                settings.TokenLifetime = TimeSpan.Parse(configuration["TOKEN_LIFETIME"]);
                settings.TokenType = configuration["TOKEN_TYPE"];
            });
            services.AddScoped<ITokenService, TokenService>();

            // User context
            services.AddScoped<UserValidator>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            // Food context
            services.AddScoped<FoodValidator>();
            services.AddScoped<IFoodRepository, FoodRepository>();
            services.AddScoped<IFoodService, FoodService>();

            // Recipe context
            services.AddScoped<RecipeValidator>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IRecipeService, RecipeService>();

            // Nutrient context
            services.AddScoped<NutrientValidator>();
            services.AddScoped<INutrientService, NutrientService>();

            // Role context
            services.AddScoped<RoleValidator>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();

            // Preparation context
            services.AddScoped<IPreparationRepository, PreparationRepository>();

            // PreparationStep context
            services.AddScoped<IPreparationStepRepository, PreparationStepRepository>();

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