using System.Collections.Generic;
using System.Linq;

namespace PollChallenge.Domain.Entities
{
    public class PollOption : Entity
    {
        public PollOption(int optionId, int pollId, string optionDescription)
        {
            OptionId = optionId;
            PollId = pollId;
            OptionDescription = optionDescription;
        }

        public int OptionId { get; private set; }
        public int PollId { get; private set; }
        public string OptionDescription { get; private set; }

        public virtual Poll Poll { get; private set; }
        public virtual IEnumerable<Vote> Votes { get; private set; }

        public static IEnumerable<PollOption> MountList(IEnumerable<string> options)
        {
            if (options == null || !options.Any())
                return null;

            var pollOptions = new List<PollOption>();

            for (var i = 0; i < options.Count(); i++)
                pollOptions.Add(new PollOption(i + 1, 0, options.ElementAt(i)));

            return pollOptions;
        }
    }
}