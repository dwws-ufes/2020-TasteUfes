using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TasteUfes.Models;
using TasteUfes.Resources;
using TasteUfes.Services.Interfaces;
using TasteUfes.Services.Notifications;

namespace TasteUfes.Controllers
{
    public class FoodsController : EntityControllerV1<Food, FoodResource>
    {
        public FoodsController(IFoodService foodService, IMapper mapper, INotificator notificator)
            : base(foodService, mapper, notificator)
        {

        }

        [HttpPost]
        public override ActionResult<FoodResource> Post([FromBody] FoodResource resource)
        {
            var mapped = Mapper.Map<Food>(resource);

            var entity = Service.Add(mapped);

            if (Notificator.HasErrors())
                return BadRequest(Errors());

            return Created(string.Empty, Mapper.Map<FoodResource>(entity));
        }
    }
}