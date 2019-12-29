using System.Collections.Generic;

namespace PollChallenge.Domain.ValueObjects
{
    public class RequestInsertPoll
    {
        public string Description { get; set; }
        public IEnumerable<string> Options { get; set; }
    }
}