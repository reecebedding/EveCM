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

        public IEnumerable<ApplicationUser> GetUsersToAddToRole(string roleName)
        {
            List<ApplicationUser> usersToAdd = new List<ApplicationUser>();
            IEnumerable<ApplicationUser> usersInSystem = _userManager.Users.ToList();
            IEnumerable<ApplicationUser> usersInRole = _userManager.GetUsersInRoleAsync(roleName).Result;

            usersToAdd.AddRange(usersInSystem.Except(usersInRole));
            return usersToAdd;
        }

        public ApplicationUser RemoveUserFromRole(ApplicationUser user, string roleName)
        {
            var identityResult =  _userManager.RemoveFromRoleAsync(user, roleName).Result;

            return user;
        }
    }
}
