using PollChallenge.Application.Interfaces;
using PollChallenge.Domain.Entities;
using PollChallenge.Domain.Interfaces.CrossCutting.Json;
using PollChallenge.Domain.Interfaces.Repositories.EFCore;
using PollChallenge.Domain.Interfaces.Services;
using PollChallenge.Infra.CrossCutting.Validation;
using System.Threading.Tasks;

namespace PollChallenge.Application.Services
{
    public class VoteAppService : BaseAppService, IVoteAppService
    {
        private readonly IVoteEFRepository _voteEFRepository;
        private readonly IPollOptionEFRepository _pollOptionEFRepository;
        private readonly ICustomContractResolver _customContractResolver;

        public VoteAppService(INotificationService notificationService,
            IVoteEFRepository voteEFRepository,
            IPollOptionEFRepository pollOptionEFRepository,
            ICustomContractResolver customContractResolver) : base(notificationService)
        {
            _voteEFRepository = voteEFRepository;
            _pollOptionEFRepository = pollOptionEFRepository;
            _customContractResolver = customContractResolver;
        }


        public async Task<object> Insert(int pollId, int optionId)
        {
            var vote = new Vote(pollId, optionId);
            if (!ExecuteValidation(new VoteValidation(), vote)) return null;

            if (await _pollOptionEFRepository.GetByKey(optionId, pollId) == null)
                return null;

            await _voteEFRepository.Insert(vote);

            return _customContractResolver.GetObjectIgnoringProperties(vote, "vote_id", "poll_option");
        }
    }
}