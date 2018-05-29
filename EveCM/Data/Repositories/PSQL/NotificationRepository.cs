using EveCM.Data.Repositories.Contracts;
using EveCM.Models.Bulletin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EveCM.Data.Repositories.PSQL
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly EveCMContext db;

        public NotificationRepository(EveCMContext context)
        {
            db = context;
        }

        public IEnumerable<Notification> GetNotifications(int count = 3)
        {
            IEnumerable<Notification> notifications = db.Notifications.OrderByDescending(x => x.Date).Take(count).ToList();
            return notifications;
        }

        public void SaveNotification(Notification notification)
        {
            db.Notifications.Add(notification);
            db.SaveChanges();
        }
    }
}
