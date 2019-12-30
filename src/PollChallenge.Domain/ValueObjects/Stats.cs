using System.Collections.Generic;

namespace PollChallenge.Domain.ValueObjects
{
    public class Stats
    {
        public Stats(int views, int optionId, int qty)
        {
            Views = views;
            Votes = new List<VoteStats> { new VoteStats(optionId, qty) };
        }

        public Stats(int views, IEnumerable<VoteStats> votes)
        {
            Views = views;
            Votes = votes;
        }

        public int Views { get; }
        public IEnumerable<VoteStats> Votes { get; }
    }
}