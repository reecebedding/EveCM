using EveCM.Models.Bulletin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EveCM.Data.Repositories.Contracts
{
    public interface IBulletinRepository
    {
        IEnumerable<Bulletin> GetBulletins(out int totalCount, int? count = null);
        Bulletin GetBulletin(int id);
        Bulletin SaveBulletin(Bulletin bulletin);
    }
}
