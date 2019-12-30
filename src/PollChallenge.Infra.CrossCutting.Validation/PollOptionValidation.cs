using FluentValidation;
using PollChallenge.Domain.Entities;

namespace PollChallenge.Infra.CrossCutting.Validation
{
    public class PollOptionValidation : AbstractValidator<PollOption>
    {
        public PollOptionValidation()
        {
            RuleFor(p => p.OptionId)
                .GreaterThan(0).WithMessage("option_id must be greater than 0");

            RuleFor(p => p.OptionDescription)
                .NotEmpty().WithMessage("option_description is required")
                .MaximumLength(50).WithMessage("option_description must be at most 50 characters");
        }
    }
}