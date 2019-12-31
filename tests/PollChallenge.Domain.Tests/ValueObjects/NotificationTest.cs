using PollChallenge.Domain.ValueObjects;
using Xunit;
using Bogus;

namespace PollChallenge.Domain.Tests.ValueObjects
{
    public class NotificationTest
    {
        [Fact(DisplayName = "Notification - instantiate class")]
        [Trait("Category", "ValueObjects")]
        public void Notification_InstantiateClass()
        {
            // Arrange & Act
            var notification = new Faker<Notification>()
                .CustomInstantiator(f => new Notification(f.Lorem.Letter(f.Random.Int(10, 1000))))
                .Generate();

            // Assert
            Assert.NotNull(notification);
            Assert.False(string.IsNullOrEmpty(notification.Message));
        }
    }
}