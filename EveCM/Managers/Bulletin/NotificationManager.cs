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
    public class NotificationManager : INotificationManager
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public NotificationManager(INotificationRepository notificationRepository, UserManager<ApplicationUser> userManager)
        {
            _notificationRepository = notificationRepository;
            _userManager = userManager;
        }

        public IEnumerable<Notification> GetNotifications(out int totalCount, int count = 3)
        {
            return _notificationRepository.GetNotifications(out totalCount, count);
        }

        public void SaveNewNotification(Notification notification, ClaimsPrincipal user = null)
        {
            if (user != null)
                notification.AuthorId = _userManager.GetUserAsync(user).Result.Id;
            
            notification.Date = DateTime.Now;
            _notificationRepository.SaveNotification(notification);
        }
    }
}
