using PollChallenge.Domain.ValueObjects;
using System.Threading.Tasks;

namespace PollChallenge.Application.Interfaces
{
    public interface IPollAppService
    {
        Task<object> Insert(RequestInsertPoll requestInsertPoll);
        Task<object> GetById(int pollId);
        Task<Stats> GetStatsById(int pollId);
    }
}