using Bogus;
using PollChallenge.Domain.Entities;
using PollChallenge.Domain.Tests.Entities.Config;
using PollChallenge.Domain.Commons;
using Xunit;
using PollChallenge.Infra.CrossCutting.Validation;

namespace PollChallenge.Domain.Tests.Entities
{
    [Collection(nameof(ConfigCollection))]
    public class PollTest
    {
        private readonly ConfigTestFixture _configTestFixture;

        public PollTest(ConfigTestFixture configTestFixture)
        {
            _configTestFixture = configTestFixture;
        }


        [Fact(DisplayName = "New poll valid")]
        [Trait("Category", "Entities")]
        public void Poll_NewPoll_Valid()
        {
            // Arrange & Act
            var isValid = _configTestFixture
                .ExecuteValidation(new PollValidation(), GetPollValid());

            // Assert
            Assert.True(isValid);
            Assert.False(_configTestFixture.HaveNotification());
        }

        [Fact(DisplayName = "New poll invalid")]
        [Trait("Category", "Entities")]
        public void Poll_NewPoll_Invalid()
        {
            // Arrange & Act
            var isValid = _configTestFixture.ExecuteValidation(new PollValidation(), GetPollInvalid());

            // Assert
            Assert.False(isValid);
            Assert.True(_configTestFixture.HaveNotification());
        }


        private static Poll GetPollValid()
        {
            return new Faker<Poll>(Constants.LanguageBogus)
                .CustomInstantiator(f =>
                    Poll.MountInsert(f.Lorem.Letter(100), f.MakeLazy(3, s => s.ToString())))
                .Generate();
        }

        private static Poll GetPollInvalid()
        {
            return new Faker<Poll>(Constants.LanguageBogus)
                .CustomInstantiator(f =>
                    Poll.MountInsert(f.Lorem.Letter(101), f.MakeLazy(3, s => s.ToString())))
                .Generate();
        }
    }
}