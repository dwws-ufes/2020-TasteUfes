using Microsoft.Extensions.DependencyInjection;
using Tasteufes.Data.Interfaces;
using TasteUfes.Data;
using TasteUfes.Data.Context;
using TasteUfes.Data.Interfaces;
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

            services.AddScoped<IFoodService, FoodService>();

            services.AddScoped<IFoodRepository, FoodRepository>();

            return services;
        }
    }
}