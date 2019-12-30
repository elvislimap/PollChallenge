using FluentValidation;
using PollChallenge.Domain.Entities;

namespace PollChallenge.Infra.CrossCutting.Validation
{
    public class PollValidation : AbstractValidator<Poll>
    {
        public PollValidation()
        {
            RuleFor(p => p.PollDescription)
                .NotEmpty().WithMessage("poll_description is required")
                .MaximumLength(100).WithMessage("poll_description must be at most 100 characters");

            RuleFor(p => p.Options)
                .NotNull().WithMessage("options is required");

            RuleForEach(p => p.Options)
                .SetValidator(new PollOptionValidation());
        }
    }
}