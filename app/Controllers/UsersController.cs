using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TasteUfes.Models;
using TasteUfes.Resources;
using TasteUfes.Services.Interfaces;
using TasteUfes.Services.Notifications;

namespace TasteUfes.Controllers
{
    public class UserControllers : EntityControllerV1<User, UserResource>
    {
        public UserControllers(IUserService userService, IMapper mapper, INotificator notificator)
            : base(userService, mapper, notificator)
        {

        }
    }
}