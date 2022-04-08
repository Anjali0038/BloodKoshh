using BloodKoshh.Areas.Identity.Data;
using BloodKoshh.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodKoshh.Controllers
{
    [Authorize]
    public class NotificationController : Controller
    {
        private INotificationRepository _notificationRepository;
        private UserManager<BloodKoshhUser> _userManager;
        public NotificationController(INotificationRepository notificationRepository,
                                        UserManager<BloodKoshhUser> userManager)
        {
            _notificationRepository = notificationRepository;
            _userManager = userManager;
        }

        public IActionResult GetNotification()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var notification = _notificationRepository.GetUserNotifications(userId);
            return Ok(new { UserNotification = notification, Count = notification.Count });
        }

        public IActionResult ReadNotification(int notificationId)
        {

            _notificationRepository.ReadNotification(notificationId, _userManager.GetUserId(HttpContext.User));

            return Ok();
        }
    }
}
