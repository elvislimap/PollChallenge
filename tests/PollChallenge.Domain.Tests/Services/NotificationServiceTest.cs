using Bogus;
using PollChallenge.Domain.Interfaces.Services;
using PollChallenge.Domain.Services;
using PollChallenge.Domain.ValueObjects;
using PollChallenge.Domain.Commons;
using Xunit;

namespace PollChallenge.Domain.Tests.Services
{
    public class NotificationServiceTest
    {
        private readonly INotificationService _notificationService;

        public NotificationServiceTest()
        {
            _notificationService = new NotificationService();
        }


        [Fact(DisplayName = "Handle and get notifications")]
        [Trait("Category", "Services")]
        public void NotificationService_HandleAndGetNotifications()
        {
            // Arrange & Act & Assert
            HandleNotification();
            Assert.Single(_notificationService.GetNotifications());
        }

        [Fact(DisplayName = "Handle and have notifications")]
        [Trait("Category", "Services")]
        public void NotificationService_HandleAndHaveNotifications()
        {
            // Arrange & Act & Assert
            HandleNotification();
            Assert.True(_notificationService.HaveNotification());
        }

        [Fact(DisplayName = "Handle and clear notifications")]
        [Trait("Category", "Services")]
        public void NotificationService_HandleAndClearNotifications()
        {
            // Arrange
            HandleNotification();

            // Act
            _notificationService.Clear();

            // Assert
            Assert.False(_notificationService.HaveNotification());
        }


        private void HandleNotification()
        {
            _notificationService.Clear();
            _notificationService.Handle(
                new Faker<Notification>(Constants.LanguageBogus)
                .CustomInstantiator(f => new Notification(f.Lorem.Letter(200)))
                .Generate());
        }
    }
}