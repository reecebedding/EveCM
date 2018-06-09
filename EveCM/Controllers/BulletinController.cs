using AutoMapper;
using EveCM.Managers.Bulletin.Contracts;
using EveCM.Models.Bulletin;
using EveCM.Models.Bulletin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EveCM.Controllers
{
    [Route("bulletin")]
    public class BulletinController : Controller
    {
        private readonly INotificationManager _notificationManager;
        private readonly IMapper _mapper;

        public BulletinController(INotificationManager notificationManager, IMapper mapper)
        {
            _notificationManager = notificationManager;
            _mapper = mapper;
        }

        [HttpPost("bulletin")]
        public IActionResult SaveNewBulletin(NotificationViewModel newNotification)
        {
            Notification notification = _mapper.Map<Notification>(newNotification);
            
            _notificationManager.SaveNewNotification(notification, User);

            return RedirectToAction("Index", "Home");
        }
    }
}