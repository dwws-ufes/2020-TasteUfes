using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TasteUfes.Data.Interfaces;
using TasteUfes.Data.Context;
using TasteUfes.Models;

namespace TasteUfes.Data
{
    public class RecipeRepository : EntityRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(ApplicationDbContext context)
            : base(context) { }
    }
}