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

        public IEnumerable<Notification> GetNotifications(out int totalCount, int count = 3)
        {
            IEnumerable<Notification> notifications = db.Notifications.OrderByDescending(x => x.Date).Take(count).ToList();
            totalCount = db.Notifications.Count();
            return notifications;
        }

        public void SaveNotification(Notification notification)
        {
            db.Notifications.Add(notification);
            db.SaveChanges();
        }
    }
}
