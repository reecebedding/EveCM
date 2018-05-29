using EveCM.Models.Bulletin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EveCM.Managers.Bulletin.Contracts
{
    public interface INotificationManager
    {
        IEnumerable<Notification> GetNotifications(int count = 3);
        void SaveNewNotification(Notification notification);
    }
}
