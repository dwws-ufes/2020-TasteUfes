using Microsoft.Extensions.DependencyInjection;
using TasteUfes.Data;
using TasteUfes.Data.Context;
using TasteUfes.Data.Interfaces;
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

            return services;
        }
    }
}