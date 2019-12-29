using System.Threading.Tasks;

namespace PollChallenge.Application.Interfaces
{
    public interface IVoteAppService
    {
        Task<object> Insert(int pollId, int optionId);
    }
}