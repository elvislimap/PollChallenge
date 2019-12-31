using Bogus;
using PollChallenge.Domain.Entities;
using PollChallenge.Domain.Tests.Entities.Config;
using PollChallenge.Domain.Commons;
using Xunit;
using PollChallenge.Infra.CrossCutting.Validation;

namespace VoteChallenge.Domain.Tests.Entities
{
    [Collection(nameof(ConfigCollection))]
    public class VoteTest
    {
        private readonly ConfigTestFixture _configTestFixture;

        public VoteTest(ConfigTestFixture configTestFixture)
        {
            _configTestFixture = configTestFixture;
        }


        [Fact(DisplayName = "New vote valid")]
        [Trait("Category", "Entities")]
        public void Vote_NewVote_Valid()
        {
            // Arrange & Act
            var isValid = _configTestFixture
                .ExecuteValidation(new VoteValidation(), GetVoteValid());

            // Assert
            Assert.True(isValid);
            Assert.False(_configTestFixture.HaveNotification());
        }

        [Fact(DisplayName = "New vote invalid")]
        [Trait("Category", "Entities")]
        public void Vote_NewVote_Invalid()
        {
            // Arrange & Act
            var isValid = _configTestFixture.ExecuteValidation(new VoteValidation(), GetVoteInvalid());

            // Assert
            Assert.False(isValid);
            Assert.True(_configTestFixture.HaveNotification());
        }


        private static Vote GetVoteValid()
        {
            return new Faker<Vote>(Constants.LanguageBogus)
                .CustomInstantiator(f => new Vote(f.Random.Int(1, 1000), f.Random.Int(1, 3)))
                .Generate();
        }

        private static Vote GetVoteInvalid()
        {
            return new Faker<Vote>(Constants.LanguageBogus)
                .CustomInstantiator(f => new Vote(f.Random.Int(-1000, -1), f.Random.Int(-3, -1)))
                .Generate();
        }
    }
}