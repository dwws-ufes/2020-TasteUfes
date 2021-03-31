using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasteUfes.Models;
using TasteUfes.Resources;
using TasteUfes.Services;
using TasteUfes.Services.Interfaces;
using TasteUfes.Services.Notifications;

namespace TasteUfes.Controllers
{
    [Authorize]
    public class UsersController : EntityApiControllerV1<User, UserResource>
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public UsersController(ITokenService tokenService,
            IUserService userService, IMapper mapper, INotificator notificator) : base(userService, mapper, notificator)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public override ActionResult<UserResource> Post([FromBody] UserResource resource)
        {
            if (!(User.Identity.IsAuthenticated && User.IsInRole("Admin")))
            {
                resource.Roles = null;
            }

            return base.Post(resource);
        }

        [HttpPut("{id}")]
        public override ActionResult<UserResource> Put([FromRoute] Guid id, [FromBody] UserResource resource)
        {
            if (id != resource?.Id)
                return NotFound();

            var user = _userService.Get(id);

            if (HasErrors())
                return NotFound();

            // Checa se o usuário logado e o requisitado são os mesmos.
            // A regra não se aplica a administradores.
            if (user.Username != User.Identity.Name && !User.IsInRole("Admin"))
                return Forbid();

            return base.Put(id, resource);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public override IActionResult Delete([FromRoute] Guid id)
        {
            return base.Delete(id);
        }

        [HttpPut("{id}/password-update")]
        public ActionResult<string> UpdatePassword([FromRoute] Guid id, [FromBody] UserPasswordResource resource)
        {
            return "In progress";
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<TokenResource> Login([FromBody] UserLoginResource resource)
        {
            var user = _userService.GetByCredentials(resource.Username, resource.Password);

            if (user == null || HasErrors())
                return NotFound(Errors());

            var token = _tokenService.GenerateAccessToken(user);

            return Mapper.Map<TokenResource>(token);
        }
    }
}