using EveCM.Models.Bulletin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EveCM.Data.Repositories.Contracts
{
    public interface IBulletinRepository
    {
        IEnumerable<Bulletin> GetBulletins(out int totalCount, int count = 3);
        void SaveBulletin(Bulletin bulletin);
    }
}
