using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EveCM.Models.Bulletin.ViewModels
{
    public class NotificationViewModel : Notification
    {
        public ApplicationUser AuthorCharacter { get; set; }
    }
}
