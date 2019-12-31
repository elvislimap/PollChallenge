using Bogus;
using FluentValidation.Results;
using PollChallenge.Application.Services;
using PollChallenge.Domain.Entities;
using PollChallenge.Domain.Services;
using PollChallenge.Infra.CrossCutting.Validation;
using System.Collections.Generic;
using Xunit;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace PollChallenge.Application.Tests.Services
{
    public class BaseAppServiceTest : BaseAppService
    {
        public BaseAppServiceTest() : base(new NotificationService()) { }

        [Fact(DisplayName = "Notify message")]
        [Trait("Category", "AppServices")]
        public void BaseAppService_NotifyMessage()
        {
            // Arrange & Act
            Notify("Test notification by message");

            // Assert
            Assert.True(HaveNotification());
        }

        [Fact(DisplayName = "Notify validation result")]
        [Trait("Category", "AppServices")]
        public void BaseAppService_NotifyValidationResult()
        {
            // Arrange
            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("test1", "error1"),
                new ValidationFailure("test2", "error2")
            };

            // Act
            Notify(new ValidationResult(validationFailures));

            // Assert
            Assert.True(HaveNotification());
        }

        [Fact(DisplayName = "Execute validation is valid")]
        [Trait("Category", "AppServices")]
        public void BaseAppService_ExecuteValidation_IsValid()
        {
            // Arrange
            var poll = new Faker<Poll>()
                .CustomInstantiator(f => Poll.MountInsert(f.Lorem.Letter(100), f.MakeLazy(3, s => s.ToString())))
                .Generate();

            // Act
            var isValid = ExecuteValidation(new PollValidation(), poll);

            // Assert
            Assert.True(isValid);
        }

        [Fact(DisplayName = "Execute validation is false")]
        [Trait("Category", "AppServices")]
        public void BaseAppService_ExecuteValidation_IsFalse()
        {
            // Arrange & Act
            var isValid = ExecuteValidation(new PollValidation(), new Poll(null));

            // Assert
            Assert.False(isValid);
        }
    }
}