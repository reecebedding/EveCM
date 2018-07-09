using EveCM.Models.Bulletin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EveCM.Managers.Bulletin.Contracts
{
    public interface IBulletinManager
    {
        IEnumerable<Models.Bulletin.Bulletin> GetBulletins(out int totalCount, int? count = null);
        Models.Bulletin.Bulletin GetBulletin(int id);
        Models.Bulletin.Bulletin SaveNewBulletin(Models.Bulletin.Bulletin bulletin, ClaimsPrincipal user);
        Models.Bulletin.Bulletin RemoveBulletin(Models.Bulletin.Bulletin bulletin);
    }
}
