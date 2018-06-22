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
        IEnumerable<Models.Bulletin.Bulletin> GetBulletins(out int totalCount, int count = 3);
        void SaveNewBulletin(Models.Bulletin.Bulletin bulletin, ClaimsPrincipal user = null);
    }
}
