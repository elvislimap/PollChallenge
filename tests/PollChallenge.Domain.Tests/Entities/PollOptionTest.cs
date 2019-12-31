using Bogus;
using PollChallenge.Domain.Entities;
using PollChallenge.Domain.Tests.Entities.Config;
using PollChallenge.Domain.Commons;
using Xunit;
using PollChallenge.Infra.CrossCutting.Validation;

namespace PollOptionChallenge.Domain.Tests.Entities
{
    [Collection(nameof(ConfigCollection))]
    public class PollOptionTest
    {
        private readonly ConfigTestFixture _configTestFixture;

        public PollOptionTest(ConfigTestFixture configTestFixture)
        {
            _configTestFixture = configTestFixture;
        }


        [Fact(DisplayName = "New pollOption valid")]
        [Trait("Category", "Entities")]
        public void PollOption_NewPollOption_Valid()
        {
            // Arrange & Act
            var isValid = _configTestFixture
                .ExecuteValidation(new PollOptionValidation(), GetPollOptionValid());

            // Assert
            Assert.True(isValid);
            Assert.False(_configTestFixture.HaveNotification());
        }

        [Fact(DisplayName = "New pollOption invalid")]
        [Trait("Category", "Entities")]
        public void PollOption_NewPollOption_Invalid()
        {
            // Arrange & Act
            var isValid = _configTestFixture.ExecuteValidation(new PollOptionValidation(), GetPollOptionInvalid());

            // Assert
            Assert.False(isValid);
            Assert.True(_configTestFixture.HaveNotification());
        }


        private static PollOption GetPollOptionValid()
        {
            return new Faker<PollOption>(Constants.LanguageBogus)
                .CustomInstantiator(f => new PollOption(f.Random.Int(1, 3), 0, f.Lorem.Letter(50)))
                .Generate();
        }

        private static PollOption GetPollOptionInvalid()
        {
            return new Faker<PollOption>(Constants.LanguageBogus)
                .CustomInstantiator(f => new PollOption(f.Random.Int(-3, -1), 0, f.Lorem.Letter(51)))
                .Generate();
        }
    }
}