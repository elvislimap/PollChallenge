using PollChallenge.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace PollChallenge.Domain.Interfaces.Repositories.EFCore
{
    public interface IPollEFRepository : IDisposable
    {
        Task Insert(Poll poll);
        Task<Poll> GetById(int pollId);
        Task<Poll> GetStatsById(int pollId);
    }
}