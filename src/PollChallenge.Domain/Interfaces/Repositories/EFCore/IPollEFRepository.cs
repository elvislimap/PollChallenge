using PollChallenge.Domain.Entities;
using PollChallenge.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace PollChallenge.Domain.Interfaces.Repositories.EFCore
{
    public interface IPollEFRepository : IDisposable
    {
        Task Insert(Poll poll);
        Task UpdateViews(Poll poll);
        Task<Poll> GetById(int pollId);
        Task<Stats> GetStatsById(int pollId);
    }
}