using PollChallenge.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace PollChallenge.Domain.Interfaces.Repositories.EFCore
{
    public interface IPollOptionEFRepository : IDisposable
    {
        Task<PollOption> GetByKey(int optionId, int pollId);
    }
}