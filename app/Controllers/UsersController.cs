using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasteUfes.Controllers.Contracts.Requests;
using TasteUfes.Controllers.Contracts.Responses;
using TasteUfes.Models;
using TasteUfes.Services.Interfaces;
using TasteUfes.Services.Notifications;

namespace TasteUfes.Controllers
{
    [Authorize]
    public class UsersController : EntityApiControllerV1<User, UserRequest, UserResponse>
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
        public override ActionResult<UserResponse> Post([FromBody] UserRequest resource)
        {
            if (!ModelState.IsValid)
            {
                resource.Password = null;
                return BadRequest(Errors(resource));
            }

            if (User.Identity.IsAuthenticated && !User.IsInRole("Admin"))
                return Forbid();

            if (!User.Identity.IsAuthenticated && !User.IsInRole("Admin"))
                resource.Roles = new List<RoleRequest>();

            return base.Post(resource);
        }

        [HttpPut("{id}")]
        public override ActionResult<UserResponse> Put([FromRoute] Guid id, [FromBody] UserRequest resource)
        {
            ModelState.Remove("Password");

            if (!ModelState.IsValid)
            {
                resource.Password = null;
                return BadRequest(Errors(resource));
            }

            if (id != resource?.Id)
                return NotFound();

            var user = _userService.Get(id);

            if (HasErrors())
                return NotFound();

            // Checa se o usuário logado e o requisitado são os mesmos.
            // A regra não se aplica a administradores.
            if (user.Username != User.Identity.Name && !User.IsInRole("Admin"))
                return Forbid();

            // Caso o usuário não seja admin, ele não tem permissão para
            // atualizar as roles.
            if (!User.IsInRole("Admin"))
                resource.Roles = Mapper.Map<IEnumerable<RoleRequest>>(user.Roles);

            var updated = _userService.Update(Mapper.Map<User>(resource));

            if (HasErrors())
                return BadRequest(Errors(resource));

            return Ok(Mapper.Map<UserResponse>(updated));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public override IActionResult Delete([FromRoute] Guid id) => base.Delete(id);

        [HttpPut("{id}/update-password")]
        public ActionResult<UserResponse> UpdatePassword([FromRoute] Guid id, [FromBody] UserPasswordRequest resource)
        {
            if (!ModelState.IsValid)
            {
                resource.OldPassword = null;
                resource.NewPassword = null;
                return BadRequest(Errors(resource));
            }

            var user = _userService.GetByUsername(User.Identity.Name);

            if (user.Id != id)
                return Forbid();

            _userService.UpdatePassword(id, resource.OldPassword, resource.NewPassword);

            if (HasErrors())
            {
                resource.OldPassword = null;
                resource.NewPassword = null;
                return BadRequest(Errors(resource));
            }

            return Mapper.Map<UserResponse>(user);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<TokenResponse> Login([FromBody] UserLoginRequest resource)
        {
            if (!ModelState.IsValid)
            {
                resource.Password = null;
                return BadRequest(Errors(resource));
            }

            var user = _userService.GetByCredentials(resource.Username, resource.Password);

            if (user == null || HasErrors())
                return NotFound(Errors());

            var token = _tokenService.GenerateAccessToken(user);

            if (HasErrors())
                return BadRequest(Errors(resource));

            return new TokenResponse
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
        public ActionResult<TokenResponse> RefreshToken([FromBody] RefreshTokenRequest resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(Errors(resource));

            var token = _tokenService.RefreshToken(resource.AccessToken, resource.RefreshToken);

            if (HasErrors())
                return BadRequest(Errors(resource));

            return new TokenResponse
            {
                UserId = token.UserId,
                TokenType = token.TokenType,
                AccessToken = token.AccessToken,
                ExpiresIn = token.AccessTokenLifetime,
                RefreshToken = token.RefreshToken
            };
        }

        [HttpGet("~/api/v1/roles")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<RoleResponse>> GetRoles([FromServices] IRoleService roleService)
        {
            return Ok(Mapper.Map<IEnumerable<RoleResponse>>(roleService.GetAll()));
        }
    }
}