using PollChallenge.Domain.Entities;
using System.Threading.Tasks;

namespace PollChallenge.Application.Interfaces
{
    public interface IVoteAppService
    {
        Task Insert(Vote vote);
    }
}