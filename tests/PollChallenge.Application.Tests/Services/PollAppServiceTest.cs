using Bogus;
using Moq;
using Moq.AutoMock;
using PollChallenge.Application.Services;
using PollChallenge.Domain.Entities;
using PollChallenge.Domain.Interfaces.CrossCutting.Json;
using PollChallenge.Domain.Interfaces.Repositories.EFCore;
using PollChallenge.Domain.ValueObjects;
using System.Threading.Tasks;
using Xunit;

namespace PollChallenge.Application.Tests.Services
{
    public class PollAppServiceTest
    {
        [Fact(DisplayName = "PollAppService - insert is valid")]
        [Trait("Category", "AppServices")]
        public async void PollAppService_Insert_IsValid()
        {
            // Arrange
            var request = new Faker<RequestInsertPoll>()
                .CustomInstantiator(f =>
                    new RequestInsertPoll(f.Lorem.Letter(100), f.MakeLazy(3, s => s.ToString())))
                .Generate();
            var mocker = new AutoMocker();

            mocker.GetMock<ICustomContractResolver>()
                .Setup(c => c.GetObjectIgnoringProperties(It.IsAny<Poll>(), It.IsAny<string[]>()))
                .Returns(new Poll(request.PollDescription));

            var pollAppService = mocker.CreateInstance<PollAppService>();

            // Act
            var result = await pollAppService.Insert(request);

            // Assert
            mocker.GetMock<IPollEFRepository>()
                .Verify(p => p.Insert(It.IsAny<Poll>()), Times.Once);
            mocker.GetMock<ICustomContractResolver>()
                .Verify(c => c.GetObjectIgnoringProperties(It.IsAny<Poll>(), It.IsAny<string[]>()), Times.Once);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "PollAppService - insert is not valid")]
        [Trait("Category", "AppServices")]
        public async void PollAppService_Insert_IsNotValid()
        {
            // Arrange
            var mocker = new AutoMocker();
            var pollAppService = mocker.CreateInstance<PollAppService>();

            // Act
            var result = await pollAppService.Insert(new RequestInsertPoll(null, null));

            // Assert
            mocker.GetMock<IPollEFRepository>()
                .Verify(p => p.Insert(It.IsAny<Poll>()), Times.Never);
            mocker.GetMock<ICustomContractResolver>()
                .Verify(c => c.GetObjectIgnoringProperties(It.IsAny<Vote>(), It.IsAny<string[]>()), Times.Never);
            Assert.Null(result);
        }

        [Fact(DisplayName = "PollAppService - get by id")]
        [Trait("Category", "AppServices")]
        public async void PollAppService_GetById()
        {
            // Arrange
            var pollId = new Faker().Random.Int(1, 10);
            var poll = new Faker<Poll>()
                .CustomInstantiator(f => Poll
                .MountInsert(f.Lorem.Letter(100), f.MakeLazy(2, s => s.ToString())))
                .Generate();
            var mocker = new AutoMocker();

            mocker.GetMock<IPollEFRepository>().Setup(p => p.GetById(pollId))
                .Returns(Task.FromResult(poll));

            mocker.GetMock<ICustomContractResolver>()
                .Setup(c => c.GetObjectIgnoringProperties(It.IsAny<ListIgnoreProperties>()))
                .Returns(new Poll(poll.PollDescription));

            var pollAppService = mocker.CreateInstance<PollAppService>();

            // Act
            var result = await pollAppService.GetById(pollId);

            // Assert
            mocker.GetMock<IPollEFRepository>()
                .Verify(p => p.GetById(It.IsAny<int>()), Times.Once);
            mocker.GetMock<IPollEFRepository>()
                .Verify(p => p.UpdateViews(It.IsAny<Poll>()), Times.Once);
            mocker.GetMock<ICustomContractResolver>()
                .Verify(c => c.GetObjectIgnoringProperties(It.IsAny<ListIgnoreProperties>()), Times.Once);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "PollAppService - get stats")]
        [Trait("Category", "AppServices")]
        public async void PollAppService_GetStats()
        {
            // Arrange
            var pollId = new Faker().Random.Int(1, 100);
            var statsFaker = new Faker<Stats>()
                .CustomInstantiator(f => new Stats(f.Random.Int(1, 1000), f.Random.Int(1, 3), f.Random.Int(1, 30)))
                .Generate();
            var mocker = new AutoMocker();

            mocker.GetMock<IPollEFRepository>().Setup(p => p.GetStatsById(pollId))
                .Returns(Task.FromResult(statsFaker));

            var pollAppService = mocker.CreateInstance<PollAppService>();

            // Act
            var result = await pollAppService.GetStatsById(pollId);

            // Assert
            mocker.GetMock<IPollEFRepository>()
                .Verify(p => p.GetStatsById(It.IsAny<int>()), Times.Once);
            Assert.NotNull(result);
        }
    }
}