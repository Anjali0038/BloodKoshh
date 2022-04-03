using BloodKoshh.Areas.Identity.Data;
using BloodKoshh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BloodKoshh.Repository
{
    public interface INotificationRepository
    {
        List<NotificationUser> GetUserNotifications(string userId);
        void Create(Notification notification, int EventId);
        void ReadNotification(int notificationId, string userId);

    }
}
