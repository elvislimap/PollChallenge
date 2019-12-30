using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using PollChallenge.Domain.Entities;
using PollChallenge.Domain.Interfaces.Services;
using PollChallenge.Domain.ValueObjects;

namespace PollChallenge.Application.Services
{
    public abstract class BaseAppService
    {
        private readonly INotificationService _notificationService;

        protected BaseAppService(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }


        protected void Notify(ValidationResult validationResult)
        {
            validationResult.Errors.ToList().ForEach(e => Notify(e.ErrorMessage));
        }

        protected void Notify(string message)
        {
            _notificationService.Handle(new Notification(message));
        }

        protected bool ExecuteValidation<TV, TE>(TV validation, TE entity)
            where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid)
                return true;

            Notify(validator);

            return false;
        }

        protected bool HaveNotification()
        {
            return _notificationService.HaveNotification();
        }
    }
}