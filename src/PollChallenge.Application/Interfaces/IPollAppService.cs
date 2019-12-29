using PollChallenge.Domain.Entities;
using PollChallenge.Domain.ValueObjects;
using System.Threading.Tasks;

namespace PollChallenge.Application.Interfaces
{
    public interface IPollAppService
    {
        Task<Poll> Insert(RequestInsertPoll requestInsertPoll);
        Task<Poll> GetById(int pollId);
        Task<Poll> GetStatsById(int pollId);
    }
}