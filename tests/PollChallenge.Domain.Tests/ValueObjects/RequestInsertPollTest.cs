using PollChallenge.Domain.ValueObjects;
using Xunit;
using Bogus;

namespace PollChallenge.Domain.Tests.ValueObjects
{
    public class RequestInsertPollTest
    {
        [Fact(DisplayName = "RequestInsertPoll - instantiate class")]
        [Trait("Category", "ValueObjects")]
        public void RequestInsertPoll_InstantiateClass()
        {
            // Arrange & Act
            var requestInsertPoll = new Faker<RequestInsertPoll>()
                .CustomInstantiator(f => new RequestInsertPoll(
                    f.Lorem.Letter(f.Random.Int(1, 100)),
                    f.MakeLazy(3, s => s.ToString())))
                .Generate();

            // Assert
            Assert.NotNull(requestInsertPoll);
            Assert.False(string.IsNullOrEmpty(requestInsertPoll.PollDescription));
            Assert.NotEmpty(requestInsertPoll.Options);
        }
    }
}