using PollChallenge.Domain.ValueObjects;
using Xunit;
using Bogus;

namespace PollChallenge.Domain.Tests.ValueObjects
{
    public class VoteStatsTest
    {
        [Fact(DisplayName = "VoteStats - instantiate class")]
        [Trait("Category", "ValueObjects")]
        public void VoteStats_InstantiateClass()
        {
            // Arrange & Act
            var voteStats = new Faker<VoteStats>()
                .CustomInstantiator(f => new VoteStats(f.Random.Int(1, 3), f.Random.Int(1, 100)))
                .Generate();

            // Assert
            Assert.NotNull(voteStats);
            Assert.True(voteStats.OptionId > 0 && voteStats.OptionId <= 3);
            Assert.True(voteStats.Qty > 0 && voteStats.Qty <= 100);
        }
    }
}