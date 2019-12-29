using System.Collections.Generic;

namespace PollChallenge.Domain.Entities
{
    public class Poll
    {
        public Poll(string description, int? views)
        {
            Description = description;
            Views = views;
        }

        public int PollId { get; private set; }
        public string Description { get; private set; }
        public int? Views { get; private set; }

        public virtual IEnumerable<PollOption> Options { get; private set; }

        public Poll GetOnlyPollId()
        {
            Description = null;
            Views = null;

            return this;
        }

        public static Poll MountInsert(string description, IEnumerable<string> options)
        {
            return new Poll(description, null)
            {
                Options = PollOption.MountList(options)
            };
        }
    }
}