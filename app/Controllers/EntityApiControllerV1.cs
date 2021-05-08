using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TasteUfes.Controllers.Contracts.Requests;
using TasteUfes.Controllers.Contracts.Responses;
using TasteUfes.Models;
using TasteUfes.Services.Interfaces;
using TasteUfes.Services.Notifications;

namespace TasteUfes.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EntityApiControllerV1<TEntity, TEntityRequest, TEntityResponse> : ControllerBase
        where TEntity : Entity, new()
        where TEntityRequest : EntityRequest, new()
        where TEntityResponse : EntityResponse, new()
    {
        protected readonly IEntityService<TEntity> Service;
        protected readonly IMapper Mapper;
        protected readonly INotificator Notificator;

        public EntityApiControllerV1(IEntityService<TEntity> service, IMapper mapper, INotificator notificator)
        {
            Service = service;
            Mapper = mapper;
            Notificator = notificator;
        }

        [HttpGet]
        public virtual ActionResult<IEnumerable<TEntityResponse>> Get()
        {
            return Ok(Mapper.Map<IEnumerable<TEntityResponse>>(Service.GetAll()));
        }

        [HttpGet("{id}")]
        public virtual ActionResult<TEntityResponse> Get([FromRoute] Guid id)
        {
            var entity = Service.Get(id);

            if (entity == null)
                return NotFound(id);

            return Ok(Mapper.Map<TEntityResponse>(entity));
        }

        [HttpPost]
        public virtual ActionResult<TEntityResponse> Post([FromBody] TEntityRequest resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(Errors(resource));

            var mapped = Mapper.Map<TEntity>(resource);

            var entity = Service.Add(mapped);

            if (Notificator.HasErrors())
                return BadRequest(Errors(resource));

            return Created(string.Empty, Mapper.Map<TEntityResponse>(entity));
        }

        [HttpPut("{id}")]
        public virtual ActionResult<TEntityResponse> Put([FromRoute] Guid id, [FromBody] TEntityRequest resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(Errors(resource));

            if (id != resource?.Id || !Service.Exists(id))
                return NotFound();

            var entity = Service.Update(Mapper.Map<TEntity>(resource));

            if (HasErrors())
                return BadRequest(Errors(resource));

            return Ok(Mapper.Map<TEntityResponse>(entity));
        }

        [HttpDelete("{id}")]
        public virtual IActionResult Delete([FromRoute] Guid id)
        {
            if (!Service.Exists(id))
                return NotFound();

            Service.Remove(id);

            if (HasErrors())
                return BadRequest(Errors());

            return NoContent();
        }

        protected bool HasErrors()
        {
            return !ModelState.IsValid || Notificator.HasErrors();
        }

        private string ToSnake(string str)
            => string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();

        protected dynamic Errors(object data = null)
        {
            foreach (var values in ModelState.Values)
            {
                foreach (var err in values.Errors)
                {
                    AddError(string.Empty, err.ErrorMessage);
                }
            }

            var errors = Notificator.GetErrors().Select(err => new ErrorResponse
            {
                Property = ToSnake(err.Property),
                Message = err.Message
            });

            if (data == null)
                return new { errors };

            return new { data, errors };
        }

        private void AddError(string property, string message)
        {
            Notificator.Handle(new Notification(NotificationType.ERROR, property, message));
        }
    }
}