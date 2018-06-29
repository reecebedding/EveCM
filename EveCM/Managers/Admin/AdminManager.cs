using EveCM.Managers.Admin.Contracts;
using EveCM.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EveCM.Managers.Admin
{
    public class AdminManager : IAdminManager
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminManager(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public ApplicationUser RemoveUserFromRole(ApplicationUser user, string roleName)
        {
            var identityResult =  _userManager.RemoveFromRoleAsync(user, roleName).Result;

            return user;
        }
    }
}
