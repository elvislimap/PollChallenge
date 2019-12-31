using Bogus;
using Moq;
using Moq.AutoMock;
using PollChallenge.Application.Services;
using PollChallenge.Domain.Entities;
using PollChallenge.Domain.Interfaces.CrossCutting.Json;
using PollChallenge.Domain.Interfaces.Repositories.EFCore;
using System.Threading.Tasks;
using Xunit;

namespace PollChallenge.Application.Tests.Services
{
    public class VoteAppServiceTest
    {
        [Fact(DisplayName = "VoteAppService - insert is valid")]
        [Trait("Category", "AppServices")]
        public async void VoteAppService_Insert_IsValid()
        {
            // Arrange
            var pollOptionFaker = new Faker<PollOption>()
                .CustomInstantiator(f =>
                    new PollOption(f.Random.Int(1, 100), f.Random.Int(1, 3), f.Lorem.Letter(50)))
                .Generate();
            var mocker = new AutoMocker();

            mocker.GetMock<IPollOptionEFRepository>()
                .Setup(p => p.GetByKey(pollOptionFaker.OptionId, pollOptionFaker.PollId))
                .Returns(Task.FromResult(pollOptionFaker));

            mocker.GetMock<ICustomContractResolver>()
                .Setup(c => c.GetObjectIgnoringProperties(It.IsAny<Vote>(), It.IsAny<string[]>()))
                .Returns(new Vote(pollOptionFaker.PollId, pollOptionFaker.OptionId));

            var voteAppService = mocker.CreateInstance<VoteAppService>();

            // Act
            var result = await voteAppService.Insert(pollOptionFaker.PollId, pollOptionFaker.OptionId);

            // Assert
            mocker.GetMock<IPollOptionEFRepository>()
                .Verify(p => p.GetByKey(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            mocker.GetMock<IVoteEFRepository>()
                .Verify(v => v.Insert(It.IsAny<Vote>()), Times.Once);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "VoteAppService - insert is not valid")]
        [Trait("Category", "AppServices")]
        public async void VoteAppService_Insert_IsNotValid()
        {
            // Arrange
            var mocker = new AutoMocker();
            var voteAppService = mocker.CreateInstance<VoteAppService>();

            // Act
            var result = await voteAppService.Insert(0, 0);

            // Assert
            mocker.GetMock<IPollOptionEFRepository>()
                .Verify(p => p.GetByKey(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
            mocker.GetMock<IVoteEFRepository>()
                .Verify(v => v.Insert(It.IsAny<Vote>()), Times.Never);
            Assert.Null(result);
        }
    }
}