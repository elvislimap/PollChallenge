using System.Collections.Generic;
using System.Linq;
using PollChallenge.Domain.Interfaces.Services;
using PollChallenge.Domain.ValueObjects;

namespace PollChallenge.Domain.Services
{
    public class NotificationService : INotificationService
    {
        private List<Notification> _notifications;

        public NotificationService()
        {
            _notifications = new List<Notification>();
        }


        public List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }

        public bool HaveNotification()
        {
            return _notifications.Any();
        }

        public void Clear()
        {
            _notifications = new List<Notification>();
        }
    }
}