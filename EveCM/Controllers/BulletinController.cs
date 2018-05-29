using EveCM.Managers.Bulletin.Contracts;
using EveCM.Models.Bulletin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EveCM.Controllers
{
    [Route("bulletin")]
    public class BulletinController : Controller
    {
        private readonly INotificationManager _notificationManager;

        public BulletinController(INotificationManager notificationManager)
        {
            _notificationManager = notificationManager;
        }

        [HttpPost("bulletin")]
        public IActionResult SaveNewBulletin([FromBody]NotificationViewModel notification)
        {
            _notificationManager.SaveNewNotification(notification);

            return RedirectToAction("Index", "Home");
        }
    }
}