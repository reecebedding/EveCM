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

        public Models.Bulletin.Bulletin GetBulletin(int id)
        {
            Models.Bulletin.Bulletin bulletin = _bulletinRepository.GetBulletin(id);
            return bulletin;
        }

        public IEnumerable<Models.Bulletin.Bulletin> GetBulletins(out int totalCount, int? count = null)
        {
            List<Models.Bulletin.Bulletin> bulletins = _bulletinRepository.GetBulletins(out totalCount, count).ToList();
            bulletins.ForEach(x => InsertAuthorDetails(x));

            return bulletins;
        }

        public Models.Bulletin.Bulletin SaveNewBulletin(Models.Bulletin.Bulletin bulletin, ClaimsPrincipal user)
        {
            ApplicationUser author = _userManager.GetUserAsync(user).Result;

            bulletin.AuthorId = author.Id;
            bulletin.Date = DateTime.Now;

            Models.Bulletin.Bulletin savedBulletin = _bulletinRepository.SaveBulletin(bulletin);
            InsertAuthorDetails(savedBulletin, author);

            return savedBulletin;
        }

        private void InsertAuthorDetails(Models.Bulletin.Bulletin bulletin)
        {
            ApplicationUser author = _userManager.FindByIdAsync(bulletin.AuthorId).Result;
            InsertAuthorDetails(bulletin, author);
        }

        private void InsertAuthorDetails(Models.Bulletin.Bulletin bulletin, ApplicationUser author)
        {
            bulletin.Author = author;
        }
    }
}
