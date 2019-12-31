using PollChallenge.Domain.ValueObjects;
using PollChallenge.Domain.Commons;
using Xunit;
using Bogus;
using AutoBogus;
using System.Collections.Generic;

namespace PollChallenge.Domain.Tests.ValueObjects
{
    public class StatsTest
    {
        [Fact(DisplayName = "Stats - instantiate class by first constructor")]
        [Trait("Category", "ValueObjects")]
        public void Stats_InstantiateClass_FirstConstructor()
        {
            // Arrange & Act
            var stats = new Faker<Stats>()
                .CustomInstantiator(f => new Stats(
                    f.Random.Int(1, 100),
                    f.Random.Int(1, 3),
                    f.Random.Int(1, 100)))
                .Generate();

            // Assert
            Assert.NotNull(stats);
            Assert.True(stats.Views > 0 && stats.Views <= 100);
            Assert.NotEmpty(stats.Votes);
        }

        [Fact(DisplayName = "Stats - instantiate class by second constructor")]
        [Trait("Category", "ValueObjects")]
        public void Stats_InstantiateClass_SecondConstructor()
        {
            // Arrange
            var faker = AutoFaker.Create(b => b.WithLocale(Constants.LanguageBogus));

            // Act
            var stats = new Faker<Stats>()
                .CustomInstantiator(f =>
                    new Stats(f.Random.Int(1, 100), faker.Generate<IEnumerable<VoteStats>>()))
                .Generate();

            // Assert
            Assert.NotNull(stats);
            Assert.True(stats.Views > 0 && stats.Views <= 100);
            Assert.NotEmpty(stats.Votes);
        }
    }
}