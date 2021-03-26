using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TasteUfes.Resources;
using TasteUfes.Services.Notifications;

namespace TasteUfes.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IMapper Mapper;
        protected readonly INotificator Notificator;

        public BaseController(IMapper mapper, INotificator notificator)
        {
            Mapper = mapper;
            Notificator = notificator;
        }

        protected bool HasErrors()
        {
            return !ModelState.IsValid || Notificator.HasErrors();
        }

        protected dynamic Errors()
        {
            foreach (var values in ModelState.Values)
            {
                foreach (var err in values.Errors)
                {
                    AddError(string.Empty, err.ErrorMessage);
                }
            }

            var errors = Notificator.GetErrors().Select(err => new ErrorResource
            {
                Property = err.Property,
                Message = err.Message
            });

            return new { errors };
        }

        private void AddError(string property, string message)
        {
            Notificator.Handle(new Notification(NotificationType.ERROR, property, message));
        }
    }
}