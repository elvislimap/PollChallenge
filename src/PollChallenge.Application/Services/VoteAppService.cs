using PollChallenge.Application.Interfaces;
using PollChallenge.Domain.Entities;
using PollChallenge.Domain.Interfaces.CrossCutting.Json;
using PollChallenge.Domain.Interfaces.Repositories.EFCore;
using System.Threading.Tasks;

namespace PollChallenge.Application.Services
{
    public class VoteAppService : IVoteAppService
    {
        private readonly IVoteEFRepository _voteEFRepository;
        private readonly IPollOptionEFRepository _pollOptionEFRepository;
        private readonly ICustomContractResolver _customContractResolver;

        public VoteAppService(IVoteEFRepository voteEFRepository,
            IPollOptionEFRepository pollOptionEFRepository,
            ICustomContractResolver customContractResolver)
        {
            _voteEFRepository = voteEFRepository;
            _pollOptionEFRepository = pollOptionEFRepository;
            _customContractResolver = customContractResolver;
        }


        public async Task<object> Insert(int pollId, int optionId)
        {
            if (await _pollOptionEFRepository.GetByKey(optionId, pollId) == null)
                return null;

            var vote = new Vote(pollId, optionId);
            await _voteEFRepository.Insert(vote);

            return _customContractResolver.GetObjectIgnoringProperties(vote, "vote_id", "poll_option");
        }
    }
}