using FluentValidation;
using FluentValidation.Results;
using PollChallenge.Domain.Entities;
using PollChallenge.Domain.Interfaces.Services;
using PollChallenge.Domain.Services;
using PollChallenge.Domain.ValueObjects;
using System.Linq;
using Xunit;

namespace PollChallenge.Domain.Tests.Entities.Config
{
    [CollectionDefinition(nameof(ConfigCollection))]
    public class ConfigCollection : ICollectionFixture<ConfigTestFixture> { }

    public class ConfigTestFixture
    {
        private readonly INotificationService _notificationService = new NotificationService();

        public bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid)
                return true;

            Notify(validator);

            return false;
        }

        public bool HaveNotification()
        {
            var haveNotification = _notificationService.HaveNotification();
            _notificationService.Clear();

            return haveNotification;
        }

        private void Notify(ValidationResult validationResult)
        {
            validationResult.Errors.ToList().ForEach(e => Notify(e.ErrorMessage));
        }

        private void Notify(string message)
        {
            _notificationService.Handle(new Notification(message));
        }
    }
}