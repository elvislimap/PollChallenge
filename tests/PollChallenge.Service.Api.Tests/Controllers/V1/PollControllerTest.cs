using Bogus;
using Microsoft.AspNetCore.Mvc;
using Moq.AutoMock;
using PollChallenge.Application.Interfaces;
using PollChallenge.Domain.Entities;
using PollChallenge.Domain.Interfaces.Services;
using PollChallenge.Domain.ValueObjects;
using PollChallenge.Service.Api.Controllers.V1;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PollChallenge.Service.Api.Tests.Controllers.V1
{
    public class PollControllerTest
    {
        [Fact(DisplayName = "PollController get by id ok")]
        [Trait("Category", "Controller")]
        public async void PollController_GetById_Ok()
        {
            // Arrange
            var pollId = new Faker().Random.Int(1, 10);
            var poll = new Faker<Poll>()
                .CustomInstantiator(f => new Poll(f.Lorem.Letter(100)))
                .Generate();
            var mocker = new AutoMocker();

            mocker.GetMock<IPollAppService>().Setup(p => p.GetById(pollId))
                .Returns(Task.FromResult(poll as object));

            var controller = mocker.CreateInstance<PollController>();

            // Act
            var result = await controller.GetById(pollId);
            var okResult = result.Result as OkObjectResult;

            // Asserts
            Assert.NotNull(okResult);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact(DisplayName = "PollController get by id not found")]
        [Trait("Category", "Controller")]
        public async void PollController_GetById_NotFound()
        {
            // Arrange
            var pollId = new Faker().Random.Int(1, 10);
            var poll = new Faker<Poll>()
                .CustomInstantiator(f => new Poll(f.Lorem.Letter(100)))
                .Generate();
            var mocker = new AutoMocker();
            var controller = mocker.CreateInstance<PollController>();

            // Act
            var result = await controller.GetById(pollId);
            var notFoundResult = result.Result as NotFoundObjectResult;

            // Asserts
            Assert.NotNull(notFoundResult);
            Assert.Equal((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
        }

        [Fact(DisplayName = "PollController get stats ok")]
        [Trait("Category", "Controller")]
        public async void PollController_GetStats_Ok()
        {
            // Arrange
            var pollId = new Faker().Random.Int(1, 10);
            var stats = new Faker<Stats>()
                .CustomInstantiator(f => new Stats(f.Random.Int(1, 100), f.Random.Int(1, 3), f.Random.Int(1, 1000)))
                .Generate();
            var mocker = new AutoMocker();

            mocker.GetMock<IPollAppService>().Setup(p => p.GetStatsById(pollId))
                .Returns(Task.FromResult(stats));

            var controller = mocker.CreateInstance<PollController>();

            // Act
            var result = await controller.GetStatsById(pollId);
            var okResult = result.Result as OkObjectResult;

            // Asserts
            Assert.NotNull(okResult);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact(DisplayName = "PollController get stats not found")]
        [Trait("Category", "Controller")]
        public async void PollController_GetStats_NotFound()
        {
            // Arrange
            var pollId = new Faker().Random.Int(1, 10);
            var stats = new Faker<Stats>()
                .CustomInstantiator(f => new Stats(f.Random.Int(1, 100), f.Random.Int(1, 3), f.Random.Int(1, 1000)))
                .Generate();
            var mocker = new AutoMocker();
            var controller = mocker.CreateInstance<PollController>();

            // Act
            var result = await controller.GetStatsById(pollId);
            var notFoundResult = result.Result as NotFoundObjectResult;

            // Asserts
            Assert.NotNull(notFoundResult);
            Assert.Equal((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
        }

        [Fact(DisplayName = "PollController insert ok")]
        [Trait("Category", "Controller")]
        public async void PollController_Insert_Ok()
        {
            // Arrange
            var request = new Faker<RequestInsertPoll>()
                .CustomInstantiator(f =>
                    new RequestInsertPoll(f.Lorem.Letter(100), f.MakeLazy(2, s => s.ToString())))
                .Generate();
            var mocker = new AutoMocker();

            mocker.GetMock<IPollAppService>().Setup(p => p.Insert(request))
                .Returns(Task.FromResult(new Faker<object>().Generate()));

            var controller = mocker.CreateInstance<PollController>();

            // Act
            var result = await controller.Insert(request);
            var okResult = result.Result as OkObjectResult;

            // Asserts
            Assert.NotNull(okResult);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact(DisplayName = "PollController insert bad request")]
        [Trait("Category", "Controller")]
        public async void PollController_Insert_BadRequest()
        {
            // Arrange
            var request = new Faker<RequestInsertPoll>()
                .CustomInstantiator(f =>
                    new RequestInsertPoll(f.Lorem.Letter(100), f.MakeLazy(2, s => s.ToString())))
                .Generate();
            var mocker = new AutoMocker();

            mocker.GetMock<INotificationService>()
                .Setup(n => n.HaveNotification()).Returns(true);

            mocker.GetMock<INotificationService>()
                .Setup(n => n.GetNotifications()).Returns(new Faker<List<Notification>>()
                .CustomInstantiator(f => new List<Notification> { new Notification(f.Lorem.Letter(200)) })
                .Generate());

            var controller = mocker.CreateInstance<PollController>();

            // Act
            var result = await controller.Insert(request);
            var badRequestResult = result.Result as BadRequestObjectResult;

            // Asserts
            Assert.NotNull(badRequestResult);
            Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);
        }

        [Fact(DisplayName = "PollController vote ok")]
        [Trait("Category", "Controller")]
        public async void PollController_Vote_Ok()
        {
            // Arrange
            var vote = new Faker<Vote>()
                .CustomInstantiator(f =>
                    new Vote(f.Random.Int(0, 10), f.Random.Int(0, 3)))
                .Generate();
            var mocker = new AutoMocker();

            mocker.GetMock<IVoteAppService>().Setup(p => p.Insert(vote.PollId, vote.OptionId))
                .Returns(Task.FromResult(new Faker<object>().Generate()));

            var controller = mocker.CreateInstance<PollController>();

            // Act
            var result = await controller.Vote(vote.PollId, vote);
            var okResult = result.Result as OkObjectResult;

            // Asserts
            Assert.NotNull(okResult);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact(DisplayName = "PollController vote bad request")]
        [Trait("Category", "Controller")]
        public async void PollController_Vote_BadRequest()
        {
            // Arrange
            var vote = new Faker<Vote>()
                .CustomInstantiator(f =>
                    new Vote(f.Random.Int(0, 10), f.Random.Int(0, 3)))
                .Generate();
            var mocker = new AutoMocker();

            mocker.GetMock<INotificationService>()
                .Setup(n => n.HaveNotification()).Returns(true);

            mocker.GetMock<INotificationService>()
                .Setup(n => n.GetNotifications()).Returns(new Faker<List<Notification>>()
                .CustomInstantiator(f => new List<Notification> { new Notification(f.Lorem.Letter(200)) })
                .Generate());

            var controller = mocker.CreateInstance<PollController>();

            // Act
            var result = await controller.Vote(vote.PollId, vote);
            var badRequestResult = result.Result as BadRequestObjectResult;

            // Asserts
            Assert.NotNull(badRequestResult);
            Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);
        }
    }
}