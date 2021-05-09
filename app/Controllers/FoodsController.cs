using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasteUfes.Models;
using TasteUfes.Services.Interfaces;
using TasteUfes.Services.Notifications;
using VDS.RDF.Writing;
using TasteUfes.Controllers.Contracts.Responses;
using TasteUfes.Controllers.Contracts.Requests;

namespace TasteUfes.Controllers
{
    public class FoodsController : EntityApiControllerV1<Food, FoodRequest, FoodResponse>
    {
        public FoodsController(IFoodService foodService, IMapper mapper, INotificator notificator)
            : base(foodService, mapper, notificator) { }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public override ActionResult<FoodResponse> Post([FromBody] FoodRequest resource)
            => base.Post(resource);

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public override ActionResult<FoodResponse> Put([FromRoute] Guid id, [FromBody] FoodRequest resource)
        {
            if (resource.NutritionFacts != null)
            {
                foreach (var nfn in resource.NutritionFacts.NutritionFactsNutrients)
                {
                    nfn.Nutrient = null;
                }
            }

            return base.Put(id, resource);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public override IActionResult Delete([FromRoute] Guid id)
            => base.Delete(id);

        [HttpGet("~/api/v1/nutrients")]
        [HttpGet("~/{culture=en-US}/api/v1/nutrients")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<NutrientResponse>> GetNutrients([FromServices] INutrientService nutrientService)
        {
            return Ok(Mapper.Map<IEnumerable<NutrientResponse>>(nutrientService.GetAll()));
        }

        [HttpGet("ld/{foodName}")]
        public ActionResult<IEnumerable<FoodResponse>> GetAllLD([FromRoute] string foodName, [FromServices] IFoodService foodService)
        {
            return Ok(Mapper.Map<IEnumerable<FoodResponse>>(foodService.GetAllLD(foodName)));
        }

        [HttpGet("ld/rdf")]
        public ActionResult GetGraph([FromServices] IFoodService foodService, [FromServices] IConfiguration configuration)
        {
            var foodUriPrefix = configuration["APP_HOST"] + configuration["APP_FOOD_PATH"];
            var foodGraph = foodService.GetGraph(foodUriPrefix);
            var stringWriter = StringWriter.Write(foodGraph, new RdfXmlWriter());

            return Content(stringWriter.ToString(), "text/xml", System.Text.Encoding.UTF8);
        }
    }
}
