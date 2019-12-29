using System.Collections.Generic;
using System.Linq;

namespace PollChallenge.Domain.Entities
{
    public class PollOption
    {
        public PollOption(int pollOptionId, int pollId, string description)
        {
            PollOptionId = pollOptionId;
            PollId = pollId;
            Description = description;
        }

        public int PollOptionId { get; private set; }
        public int PollId { get; private set; }
        public string Description { get; private set; }

        public virtual Poll Poll { get; private set; }
        public virtual IEnumerable<Vote> Votes { get; private set; }

        public static IEnumerable<PollOption> MountList(IEnumerable<string> options)
        {
            var pollOptions = new List<PollOption>();

            for (int i = 0; i < options.Count(); i++)
                pollOptions.Add(new PollOption(i + 1, 0, options.ElementAt(i)));

            return pollOptions;
        }
    }
}