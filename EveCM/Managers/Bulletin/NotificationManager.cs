using EveCM.Data.Repositories.Contracts;
using EveCM.Managers.Bulletin.Contracts;
using EveCM.Models.Bulletin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EveCM.Managers.Bulletin
{
    public class NotificationManager : INotificationManager
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationManager(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public IEnumerable<Notification> GetNotifications(int count = 3)
        {
            return _notificationRepository.GetNotifications(count);
        }

        public void SaveNewNotification(Notification notification)
        {
            _notificationRepository.SaveNotification(notification);
        }
    }
}
