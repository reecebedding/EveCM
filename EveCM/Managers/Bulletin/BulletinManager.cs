using EveCM.Data.Repositories.Contracts;
using EveCM.Managers.Bulletin.Contracts;
using EveCM.Models;
using EveCM.Models.Bulletin;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EveCM.Managers.Bulletin
{
    public class BulletinManager : IBulletinManager
    {
        private readonly IBulletinRepository _bulletinRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public BulletinManager(IBulletinRepository bulletinRepository, UserManager<ApplicationUser> userManager)
        {
            _bulletinRepository = bulletinRepository;
            _userManager = userManager;
        }

        public IEnumerable<Models.Bulletin.Bulletin> GetBulletins(out int totalCount, int count = 3)
        {
            List<Models.Bulletin.Bulletin> bulletins = _bulletinRepository.GetBulletins(out totalCount, count).ToList();
            bulletins.ForEach(x => InsertAuthorDetails(x));

            return bulletins;
        }

        public void SaveNewBulletin(Models.Bulletin.Bulletin bulletin, ClaimsPrincipal user = null)
        {
            if (user != null)
                bulletin.AuthorId = _userManager.GetUserAsync(user).Result.Id;
            
            bulletin.Date = DateTime.Now;
            _bulletinRepository.SaveBulletin(bulletin);
        }

        private void InsertAuthorDetails(Models.Bulletin.Bulletin bulletin)
        {
            bulletin.Author = _userManager.FindByIdAsync(bulletin.AuthorId).Result;
        }
    }
}
