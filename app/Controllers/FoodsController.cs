using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasteUfes.Models;
using TasteUfes.Services.Interfaces;
using TasteUfes.Services.Notifications;
using VDS.RDF;
using VDS.RDF.Nodes;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
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

        private string GetStringOrDefault(INode node, string defaultValue = null)
        {
            try
            {
                return node.AsValuedNode().AsString();
            }
            catch (Exception)
            {
                if (String.IsNullOrEmpty(defaultValue))
                    return node.ToString();

                return defaultValue;
            }
        }

        private double GetDoubleOrDefault(INode node, double defaultValue = 0)
        {
            if (node == null || node.NodeType != NodeType.Literal)
                return defaultValue;

            try
            {
                return node.AsValuedNode().AsDouble();
            }
            catch (Exception)
            {
                var doubleValue = 0.0;
                var tryParse = Double.TryParse(node.AsValuedNode().AsString(), out doubleValue);

                return tryParse ? doubleValue : 0.0;
            }
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
