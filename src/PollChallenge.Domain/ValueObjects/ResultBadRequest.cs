using System.Collections.Generic;

namespace PollChallenge.Domain.ValueObjects
{
    public class ResultBadRequest : BaseResult
    {
        public ResultBadRequest(IEnumerable<string> errors)
        {
            Success = false;
            Errors = errors;
        }

        public IEnumerable<string> Errors { get; }
    }
}