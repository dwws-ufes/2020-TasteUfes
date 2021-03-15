using System.Linq;
using Microsoft.EntityFrameworkCore;
using TasteUfes.Data.Context.Extensions;
using TasteUfes.Models;
using TasteUfes.Seeders;

namespace TasteUfes.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Food>()
                .HasIndex(f => f.Name)
                .IsUnique();

            modelBuilder.Entity<Nutrient>()
                .HasIndex(n => n.Name)
                .IsUnique();

            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<UserRole>(
                    ur => ur
                        .HasOne(ur => ur.Role)
                        .WithMany()
                        .HasForeignKey("RoleId"),
                    ur => ur
                        .HasOne(ur => ur.User)
                        .WithMany()
                        .HasForeignKey("UserId"))
                .ToTable("UserRole")
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Seed<Nutrient>();
            modelBuilder.Seed<Role>();

            /**
             * Por padrão a engine do SQLite mapeia "string" para "text" sem especificar o tamanho máximo.
             * ref: https://www.devart.com/dotconnect/sqlite/docs/DataTypeMapping.html
             *
             * Abaixo há uma forma de resolver este problema, mas manteremos o mapeamento padrão da engine.
             * modelBuilder.VarcharMaxLengthWhenUndefined(255);
             */
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            modelBuilder.OnDeleteClientSetNull();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Food> Foods { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Nutrient> Nutrients { get; set; }
        public DbSet<NutritionFacts> NutritionFacts { get; set; }
        public DbSet<NutritionFactsNutrients> NutritionFactsNutrients { get; set; }
        public DbSet<Preparation> Preparations { get; set; }
        public DbSet<PreparationStep> PreparationSteps { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}