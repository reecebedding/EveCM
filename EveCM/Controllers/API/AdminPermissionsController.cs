using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EveCM.Managers.Admin.Contracts;
using EveCM.Models;
using EveCM.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EveCM.Controllers.API
{
    [Route("api/admin")]
    [Authorize(Roles = "admin")]
    public class AdminPermissionsController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAdminManager _adminManager;
        private readonly IMapper _mapper;

        public AdminPermissionsController(
            RoleManager<IdentityRole> roleManager, 
            UserManager<ApplicationUser> userManager, 
            IAdminManager adminManager,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _adminManager = adminManager;
            _mapper = mapper;
        }

        [Route("permissions")]
        public IActionResult GetPermissions()
        {
            PermissionsDto permissions = new PermissionsDto()
            {
                Roles = _roleManager.Roles.Select(x => x.Name).ToList()
            };

            return Ok(permissions);
        }

        [Route("permissions/role/{role}")]
        public IActionResult GetRoleInformation(string role)
        {
            IEnumerable<ApplicationUser> usersInRole = _userManager.GetUsersInRoleAsync(role).Result;

            RoleInformationDto roleInformation = new RoleInformationDto()
            {
                Name = role,
                Users = _mapper.Map<IEnumerable<UserInRoleDto>>(usersInRole)
            };

            return Ok(roleInformation);
        }

        [Route("permissions/role/{role}/user/{userId}")]
        [HttpDelete]
        public IActionResult RemoveUserFromRole(string role, string userId)
        {
            ApplicationUser user = _userManager.FindByIdAsync(userId).Result;
            _adminManager.RemoveUserFromRole(user, role);

            ApplicationUserDto userDto = _mapper.Map<ApplicationUserDto>(user);

            return Ok(userDto);
        }
    }
}