using EveCM.Data.Repositories.Contracts;
using EveCM.Models.Bulletin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EveCM.Data.Repositories.PSQL
{
    public class BulletinRepository : IBulletinRepository
    {
        private readonly EveCMContext db;

        public BulletinRepository(EveCMContext context)
        {
            db = context;
        }

        public IEnumerable<Bulletin> GetBulletins(out int totalCount, int count = 3)
        {
            IEnumerable<Bulletin> bulletins = db.Bulletins.OrderByDescending(x => x.Date).Take(count).ToList();
            totalCount = db.Bulletins.Count();
            return bulletins;
        }

        public void SaveBulletin(Bulletin bulletin)
        {
            db.Bulletins.Add(bulletin);
            db.SaveChanges();
        }
    }
}
