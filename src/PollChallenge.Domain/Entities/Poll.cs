using System.Collections.Generic;

namespace PollChallenge.Domain.Entities
{
    public class Poll
    {
        public Poll(int pollId, string description, int? views)
        {
            PollId = pollId;
            Description = description;
            Views = views;
        }

        public int PollId { get; private set; }
        public string Description { get; private set; }
        public int? Views { get; private set; }

        public virtual IEnumerable<PollOption> Options { get; private set; }
    }
}