using AutoMapper;
using EveCM.Data.Repositories.Contracts;
using EveCM.Managers.Bulletin.Contracts;
using EveCM.Models;
using EveCM.Models.Bulletin.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EveCM.Components
{
    public class NotificationsViewComponent : ViewComponent
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationManager _notificationManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public NotificationsViewComponent(INotificationManager notificationManager, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _notificationManager = notificationManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<NotificationViewModel> notifications = new List<NotificationViewModel>();
            if (User.Identity.IsAuthenticated)
            {
                notifications = _mapper.Map<IEnumerable<NotificationViewModel>>(_notificationManager.GetNotifications()).ToList();
                notifications.ForEach(x => x.AuthorCharacter = _userManager.FindByIdAsync(x.AuthorId).Result);
            }
            return View("~/Views/Shared/Components/Bulletin/Notifications.cshtml", notifications);
        }
    }
}
