using PollChallenge.Domain.ValueObjects;
using System.Collections.Generic;

namespace PollChallenge.Domain.Interfaces.Services
{
    public interface INotificationService
    {
        void Handle(Notification notification);
        List<Notification> GetNotifications();
        bool HaveNotification();
        void Clear();
    }
}