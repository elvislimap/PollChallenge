using System.Collections.Generic;

namespace PollChallenge.Domain.Entities
{
    public class Poll : Entity
    {
        public Poll(string pollDescription)
        {
            PollDescription = pollDescription;
        }

        public int PollId { get; private set; }
        public string PollDescription { get; private set; }
        public int Views { get; private set; }

        public virtual IEnumerable<PollOption> Options { get; private set; }

        public static Poll MountInsert(string description, IEnumerable<string> options)
        {
            return new Poll(description)
            {
                Options = PollOption.MountList(options)
            };
        }

        public static Poll IncreaseViews(Poll poll)
        {
            if (poll == null)
                return null;

            poll.Views++;
            return poll;
        }
    }
}