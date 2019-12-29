namespace PollChallenge.Domain.Entities
{
    public class Vote
    {
        public Vote(int pollId, int optionId)
        {
            PollId = pollId;
            OptionId = optionId;
        }

        public int VoteId { get; private set; }
        public int PollId { get; private set; }
        public int OptionId { get; private set; }

        public virtual PollOption PollOption { get; private set; }
    }
}