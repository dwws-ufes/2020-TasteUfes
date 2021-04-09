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
            if (!ModelState.IsValid)
                return BadRequest(Errors(resource));

            if (!(User.Identity.IsAuthenticated && User.IsInRole("Admin")))
                resource.Roles = null;

            return base.Post(resource);
        }

        [HttpPut("{id}")]
        public override ActionResult<UserResource> Put([FromRoute] Guid id, [FromBody] UserResource resource)
        {
            ModelState.Remove("Password");

            if (!ModelState.IsValid)
                return BadRequest(Errors(resource));

            if (id != resource?.Id)
                return NotFound();

            var user = _userService.Get(id);

            if (HasErrors())
                return NotFound();

            // Checa se o usuário logado e o requisitado são os mesmos.
            // A regra não se aplica a administradores.
            if (user.Username != User.Identity.Name && !User.IsInRole("Admin"))
                return Forbid();

            var updated = Service.Update(Mapper.Map<User>(resource));

            if (HasErrors())
                return BadRequest(Errors(resource));

            return Ok(Mapper.Map<UserResource>(updated));
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
            if (!ModelState.IsValid)
                return BadRequest(Errors(resource));

            return "In progress";
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<TokenResource> Login([FromBody] UserLoginResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(Errors(resource));

            var user = _userService.GetByCredentials(resource.Username, resource.Password);

            if (user == null || HasErrors())
                return NotFound(Errors());

            var token = _tokenService.GenerateAccessToken(user);

            if (HasErrors())
                return BadRequest(Errors(resource));

            return new TokenResource
            {
                UserId = token.UserId,
                TokenType = token.TokenType,
                AccessToken = token.AccessToken,
                ExpiresIn = token.AccessTokenLifetime,
                RefreshToken = token.RefreshToken
            };
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public ActionResult<TokenResource> RefreshToken([FromBody] RefreshTokenResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(Errors(resource));

            var token = _tokenService.RefreshToken(resource.AccessToken, resource.RefreshToken);

            if (HasErrors())
                return BadRequest(Errors(resource));

            return new TokenResource
            {
                UserId = token.UserId,
                TokenType = token.TokenType,
                AccessToken = token.AccessToken,
                ExpiresIn = token.AccessTokenLifetime,
                RefreshToken = token.RefreshToken
            };
        }
    }
}