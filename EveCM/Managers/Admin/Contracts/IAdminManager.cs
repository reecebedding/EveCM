using EveCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EveCM.Managers.Admin.Contracts
{
    public interface IAdminManager
    {
        ApplicationUser RemoveUserFromRole(ApplicationUser user, string roleName);
        IEnumerable<ApplicationUser> GetUsersToAddToRole(string roleName);
    }
}
