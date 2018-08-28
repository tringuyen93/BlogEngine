using AutoMapper;
using BlogEngine.Data.Entities;
using BlogEngine.Service.Interfaces;
using BlogEngine.WebApi.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogEngine.Utility.Authorization;
using Microsoft.AspNetCore.Authorization;
using OpenIddict.Validation;

namespace BlogEngine.WebApi.Controllers
{
    //[Authorize(AuthenticationSchemes = OpenIddictValidationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        public AccountController(IMapper mapper, IAccountService accountService, IAuthorizationService authorizationService)
        {
            _accountService = accountService;
            _authorizationService = authorizationService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(null);
        }

        [HttpPost("roles")]
        [ProducesResponseType(201, Type = typeof(RoleViewModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateRole([FromBody] RoleViewModel role)
        {
            if (ModelState.IsValid)
            {
                if (role == null)
                    return BadRequest($"{nameof(role)} cannot be null");

                role.Id = Guid.NewGuid().ToString();
                foreach(var s in role.Permissions)
                {
                    s.RoleId = role.Id;
                }
                var appRole = _mapper.Map<Role>(role);

                var result = await _accountService.CreateRoleAsync(appRole, role.Permissions?.Select(p => p.Value).ToArray());
                if (result.Item1)
                {
                    return Ok();
                }

                AddErrors(result.Item2);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("users")]
        //[Authorize(Policies.ManageAllUsersPolicy)]
        [ProducesResponseType(201, Type = typeof(UserViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> CreateUser([FromBody] UserEditViewModel user)
        {
            if (!(await _authorizationService.AuthorizeAsync(this.User, Tuple.Create(user.Roles, new string[] { }),
                Policies.AssignAllowedRolesPolicy)).Succeeded)
            {
                return new ChallengeResult();
            }
            if (ModelState.IsValid)
            {
                if (user == null)
                    return BadRequest($"{nameof(user)} cannot be null");


                User appUser = _mapper.Map<User>(user);
                var result = await _accountService.CreateUserAsync(appUser, user.Roles, user.NewPassword);
                if (result.Item1)
                {
                    return Ok();
                }

                AddErrors(result.Item2);
            }

            return BadRequest(ModelState);
        }
        #region Private
        private async Task<RoleViewModel> GetRoleViewModelHelper(string roleName)
        {
            var role = await _accountService.GetRoleLoadRelatedAsync(roleName);
            if (role != null)
                return _mapper.Map<RoleViewModel>(role);


            return null;
        }

        private async Task<UserViewModel> GetUserViewModelHelper(string userId)
        {
            var userAndRoles = await _accountService.GetUserAndRolesAsync(userId);
            if (userAndRoles == null)
                return null;

            var userVM = _mapper.Map<UserViewModel>(userAndRoles.Item1);
            userVM.Roles = userAndRoles.Item2;

            return userVM;
        }

        private void AddErrors(IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }
        #endregion
    }
}