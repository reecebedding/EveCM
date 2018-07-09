using EveCM.Data.Repositories.Contracts;
using EveCM.Models.Bulletin;
using Microsoft.EntityFrameworkCore;
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

        public void Detach(Bulletin bulletin)
        {
            db.Entry(bulletin).State = EntityState.Detached;
        }

        public Bulletin GetBulletin(int id)
        {
            Bulletin bulletin = db.Bulletins.Where(x => x.Id == id).FirstOrDefault();
            return bulletin;
        }

        public IEnumerable<Bulletin> GetBulletins(out int totalCount, int? count = null)
        {
            var query = db.Bulletins.OrderByDescending(x => x.CreatedDate).AsQueryable();
            if (count != null)
                query = query.Take((int)count);

            IEnumerable<Bulletin> bulletins = query.ToList();
            totalCount = db.Bulletins.Count();

            return bulletins;
        }

        public Bulletin RemoveBulletin(Bulletin bulletin)
        {
            Bulletin bulletinRemoved = db.Bulletins.Remove(bulletin).Entity;
            db.SaveChanges();

            return bulletinRemoved;
        }

        public Bulletin ReplaceBulletin(Bulletin bulletin)
        {
            if (bulletin.Id != 0)
            {
                Bulletin bulletinReplaced = db.Bulletins.Update(bulletin).Entity;
                db.SaveChanges();
                return bulletinReplaced;
            }
            else
                throw new Exception("Cannot update a bulletin with no known id.");
        }

        public Bulletin SaveBulletin(Bulletin bulletin)
        {
            Bulletin savedBulletin = db.Bulletins.Add(bulletin).Entity;
            db.SaveChanges();

            return savedBulletin;
        }
    }
}
