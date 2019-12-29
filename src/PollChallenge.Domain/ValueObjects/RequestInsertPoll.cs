using System.Collections.Generic;

namespace PollChallenge.Domain.ValueObjects
{
    public class RequestInsertPoll
    {
        public RequestInsertPoll(string pollDescription, IEnumerable<string> options)
        {
            PollDescription = pollDescription;
            Options = options;
        }

        public string PollDescription { get; }
        public IEnumerable<string> Options { get; }
    }
}