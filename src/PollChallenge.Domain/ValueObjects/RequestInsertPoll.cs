using System.Collections.Generic;

namespace PollChallenge.Domain.ValueObjects
{
    public class RequestInsertPoll
    {
        public RequestInsertPoll(string poll, IEnumerable<string> options)
        {
            PollDescription = poll;
            Options = options;
        }

        public string PollDescription { get; }
        public IEnumerable<string> Options { get; }
    }
}