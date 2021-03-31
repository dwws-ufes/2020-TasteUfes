using AutoMapper;
using TasteUfes.Models;
using TasteUfes.Resources;

namespace Tacaro.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Food, FoodResource>().ReverseMap();
            CreateMap<Ingredient, IngredientResource>().ReverseMap();
            CreateMap<Nutrient, NutrientResource>().ReverseMap();
            CreateMap<NutritionFactsNutrients, NutritionFactsNutrientsResource>().ReverseMap();
            CreateMap<NutritionFacts, NutritionFactsResource>().ReverseMap();
            CreateMap<Preparation, PreparationResource>().ReverseMap();
            CreateMap<PreparationStep, PreparationStepResource>().ReverseMap();
            CreateMap<Recipe, RecipeResource>().ReverseMap();
            CreateMap<Role, RoleResource>().ReverseMap();
            CreateMap<User, UserResource>()
                .ForMember(u => u.Password, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Token, TokenResource>().ReverseMap();
        }
    }
}