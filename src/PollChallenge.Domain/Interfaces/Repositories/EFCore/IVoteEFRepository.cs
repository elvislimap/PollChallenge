using PollChallenge.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace PollChallenge.Domain.Interfaces.Repositories.EFCore
{
    public interface IVoteEFRepository : IDisposable
    {
        Task Insert(Vote vote);
    }
}