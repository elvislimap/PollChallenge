namespace PollChallenge.Domain.Entities
{
    public class Vote
    {
        public Vote(int pollId, int pollOptionId)
        {
            PollId = pollId;
            PollOptionId = pollOptionId;
        }

        public int VoteId { get; private set; }
        public int PollId { get; private set; }
        public int PollOptionId { get; private set; }

        public virtual PollOption PollOption { get; private set; }
    }
}