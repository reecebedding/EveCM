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

        public Bulletin GetBulletin(int id)
        {
            Bulletin bulletin = db.Bulletins.Where(x => x.Id == id).FirstOrDefault();
            return bulletin;
        }

        public IEnumerable<Bulletin> GetBulletins(out int totalCount, int? count = null)
        {
            var query = db.Bulletins.OrderByDescending(x => x.Date).AsQueryable();
            if (count != null)
                query = query.Take((int)count);

            IEnumerable<Bulletin> bulletins = query.ToList();
            totalCount = db.Bulletins.Count();

            return bulletins;
        }

        public Bulletin SaveBulletin(Bulletin bulletin)
        {
            Bulletin savedBulletin = db.Bulletins.Add(bulletin).Entity;
            db.SaveChanges();

            return savedBulletin;
        }
    }
}
