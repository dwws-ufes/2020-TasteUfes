using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TasteUfes.Configurations;
using TasteUfes.Data.Context;

namespace TasteUfes
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services
                .ResolveDependencyInjections(Configuration)
                .AddAutoMapper(typeof(Startup));

            services
                .AddCors(options =>
                {
                    options.AddPolicy("TastePolicy", builder => builder
                        .WithOrigins("https://vue-tasteufes.herokuapp.com", "http://localhost:8080")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .Build());
                })
                .AddAuthConfig(Configuration["SECRET_KEY"]);

            services.AddControllers();
            services.AddHealthChecks();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TasteUfes", Version = "v1" }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHsts();
            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("TastePolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            app.UseHealthChecks("{culture=en-US}/api/v1/health");

            var requestLocalization = new RequestLocalizationOptions
            {
                SupportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("pt-BR")
                },
                SupportedUICultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("pt-BR")
                },
                DefaultRequestCulture = new RequestCulture("en-US")
            };

            requestLocalization.RequestCultureProviders.Insert(0, new UrlRequestCultureProvider());

            app.UseRequestLocalization(requestLocalization);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
