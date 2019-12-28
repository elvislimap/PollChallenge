namespace PollChallenge.Domain.Entities
{
    public class PollOption
    {
        public PollOption(int pollOptionId, int pollId, string description, int? votes)
        {
            PollOptionId = pollOptionId;
            PollId = pollId;
            Description = description;
            Votes = votes;
        }

        public int PollOptionId { get; private set; }
        public int PollId { get; private set; }
        public string Description { get; private set; }
        public int? Votes { get; private set; }

        public virtual Poll Poll { get; private set; }
    }
}