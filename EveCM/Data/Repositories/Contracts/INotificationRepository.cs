using EveCM.Models.Bulletin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EveCM.Data.Repositories.Contracts
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetNotifications(out int totalCount, int count = 3);
        void SaveNotification(Notification notification);
    }
}
