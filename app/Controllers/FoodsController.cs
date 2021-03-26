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
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FoodsController : BaseController
    {
        private readonly IFoodService _foodService;

        public FoodsController(IFoodService foodService, IMapper mapper, INotificator notificator)
            : base(mapper, notificator)
        {
            _foodService = foodService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<FoodResource>> Get()
        {
            return Ok(Mapper.Map<IEnumerable<FoodResource>>(_foodService.GetAll()));
        }

        [HttpGet("{id}")]
        public ActionResult<FoodResource> Get([FromRoute] Guid id)
        {
            var food = _foodService.Get(id);

            if (food == null)
                return NotFound();

            return Ok(Mapper.Map<FoodResource>(food));
        }

        [HttpPost]
        public ActionResult<FoodResource> Post([FromBody] FoodResource resource)
        {
            var mapped = Mapper.Map<Food>(resource);

            var food = _foodService.Add(mapped);

            if (Notificator.HasErrors())
                return BadRequest(Errors());

            return Created(string.Empty, Mapper.Map<FoodResource>(food));
        }

        [HttpPut("{id}")]
        public ActionResult<FoodResource> Update([FromRoute] Guid id, [FromBody] FoodResource resource)
        {
            if (id != resource?.Id)
            {
                return NotFound();
            }

            var food = _foodService.Update(Mapper.Map<Food>(resource));

            if (HasErrors())
                return BadRequest(Errors());

            return Ok(Mapper.Map<FoodResource>(food));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            _foodService.Remove(id);

            if (HasErrors())
                return BadRequest(Errors());

            return NoContent();
        }
    }
}