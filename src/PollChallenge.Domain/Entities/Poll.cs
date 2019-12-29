using System.Collections.Generic;

namespace PollChallenge.Domain.Entities
{
    public class Poll
    {
        public Poll(string pollDescription, int? views)
        {
            PollDescription = pollDescription;
            Views = views;
        }

        public int PollId { get; private set; }
        public string PollDescription { get; private set; }
        public int? Views { get; private set; }

        public virtual IEnumerable<PollOption> Options { get; private set; }

        public static Poll MountInsert(string description, IEnumerable<string> options)
        {
            return new Poll(description, null)
            {
                Options = PollOption.MountList(options)
            };
        }
    }
}