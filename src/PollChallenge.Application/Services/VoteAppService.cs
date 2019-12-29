using PollChallenge.Application.Interfaces;
using PollChallenge.Domain.Entities;
using PollChallenge.Domain.Interfaces.Repositories.EFCore;
using System.Threading.Tasks;

namespace PollChallenge.Application.Services
{
    public class VoteAppService : IVoteAppService
    {
        private readonly IVoteEFRepository _voteEFRepository;

        public VoteAppService(IVoteEFRepository voteEFRepository)
        {
            _voteEFRepository = voteEFRepository;
        }


        public async Task Insert(Vote vote)
        {
            // TODO: Implementar Validações

            await _voteEFRepository.Insert(vote);
        }
    }
}