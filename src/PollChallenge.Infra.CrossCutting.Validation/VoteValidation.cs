using FluentValidation;
using PollChallenge.Domain.Entities;

namespace PollChallenge.Infra.CrossCutting.Validation
{
    public class VoteValidation : AbstractValidator<Vote>
    {
        public VoteValidation()
        {
            RuleFor(p => p.PollId)
                .GreaterThan(0).WithMessage("poll_id must be greater than 0");

            RuleFor(p => p.OptionId)
                .GreaterThan(0).WithMessage("option_id must be greater than 0");
        }
    }
}