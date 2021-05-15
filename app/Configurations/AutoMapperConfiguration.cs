using AutoMapper;
using TasteUfes.Controllers.Contracts.Requests;
using TasteUfes.Controllers.Contracts.Responses;
using TasteUfes.Models;

namespace TasteUfes.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<FoodRequest, Food>();
            CreateMap<IngredientRequest, Ingredient>();
            CreateMap<NutrientRequest, Nutrient>();
            CreateMap<NutritionFactsNutrientsRequest, NutritionFactsNutrients>();
            CreateMap<NutritionFactsRequest, NutritionFacts>();
            CreateMap<PreparationRequest, Preparation>();
            CreateMap<PreparationStepRequest, PreparationStep>();
            CreateMap<RecipeRequest, Recipe>();
            CreateMap<AnonymousRecipeRequest, Recipe>();
            CreateMap<RoleRequest, Role>();
            CreateMap<UserRequest, User>();

            CreateMap<Food, FoodResponse>();
            CreateMap<Ingredient, IngredientResponse>();
            CreateMap<Nutrient, NutrientResponse>();
            CreateMap<NutritionFactsNutrients, NutritionFactsNutrientsResponse>();
            CreateMap<NutritionFacts, NutritionFactsResponse>();
            CreateMap<Preparation, PreparationResponse>();
            CreateMap<PreparationStep, PreparationStepResponse>();
            CreateMap<Recipe, RecipeResponse>();
            CreateMap<Recipe, AnonymousRecipeResponse>();
            CreateMap<Role, RoleResponse>();
            CreateMap<User, UserResponse>()
                .ForMember(u => u.Password, opt => opt.Ignore());
        }
    }
}