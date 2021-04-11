using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TasteUfes.Models;
using TasteUfes.Resources;
using TasteUfes.Services.Interfaces;
using TasteUfes.Services.Notifications;

namespace TasteUfes.Controllers
{
    public class NutrientsController : EntityApiControllerV1<Nutrient, NutrientResource>
    {
        public NutrientsController(INutrientService service, IMapper mapper, INotificator notificator)
            : base(service, mapper, notificator) { }

        public override ActionResult<NutrientResource> Post([FromBody] NutrientResource resource) => NotFound();
        public override ActionResult<NutrientResource> Put([FromRoute] Guid id, [FromBody] NutrientResource resource) => NotFound();
        public override IActionResult Delete([FromRoute] Guid id) => NotFound();
    }
}