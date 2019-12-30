namespace PollChallenge.Domain.ValueObjects
{
    public class VoteStats
    {
        public VoteStats(int optionId, int qty)
        {
            OptionId = optionId;
            Qty = qty;
        }

        public int OptionId { get; }
        public int Qty { get; }
    }
}