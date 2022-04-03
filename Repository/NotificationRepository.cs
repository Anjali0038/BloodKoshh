using BloodKoshh.Data;
using BloodKoshh.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BloodKoshh.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        public class SignalServer : Hub
        {

        }
        public BloodKoshhContext _context { get; }
        public IWatchListRepository _watchlistRepository { get; }

        private IHubContext<SignalServer> _hubContext;

        public NotificationRepository(BloodKoshhContext context,
                                        IWatchListRepository watchlistRepository,
                                        IHubContext<SignalServer> hubContext)
        {
            _context = context;
            _watchlistRepository = watchlistRepository;
            _hubContext = hubContext;
        }

        public void Create(Notification notification, int eventid)
        {
            _context.Notifications.Add(notification);
            _context.SaveChanges();

            //TODO: Assign notification to users
            var watchlists = _watchlistRepository.GetWatchlistFromEventId(eventid);
            foreach (var watchlist in watchlists)
            {
                var userNotification = new NotificationUser();
                userNotification.UserId = watchlist.UserId;
                userNotification.NotificationId = notification.Id;

                _context.UserNotifications.Add(userNotification);
                _context.SaveChanges();
            }

            //_hubContext.Clients.All.InvokeAsync("displayNotification", "");
        }

        public List<NotificationUser> GetUserNotifications(string userId)
        {
            return _context.UserNotifications.Where(u => u.UserId.Equals(userId) && !u.IsRead)
                                            .Include(n => n.Notification)
                                            .ToList();
        }

        public void ReadNotification(int notificationId, string userId)
        {
            var notification = _context.UserNotifications
                                        .FirstOrDefault(n => n.UserId.Equals(userId)
                                        && n.NotificationId == notificationId);
            notification.IsRead = true;
            _context.UserNotifications.Update(notification);
            _context.SaveChanges();
        }
    }
}
