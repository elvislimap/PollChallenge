using System.Linq;
using System.Threading.Tasks;
using PollChallenge.Application.Interfaces;
using PollChallenge.Domain.Entities;
using PollChallenge.Domain.Interfaces.CrossCutting.Json;
using PollChallenge.Domain.Interfaces.Repositories.EFCore;
using PollChallenge.Domain.ValueObjects;

namespace PollChallenge.Application.Services
{
    public class PollAppService : IPollAppService
    {
        private readonly IPollEFRepository _pollEFRepository;
        private readonly ICustomContractResolver _customContractResolver;

        public PollAppService(IPollEFRepository pollEFRepository, ICustomContractResolver customContractResolver)
        {
            _pollEFRepository = pollEFRepository;
            _customContractResolver = customContractResolver;
        }


        public async Task<object> Insert(RequestInsertPoll requestInsertPoll)
        {
            // TODO: Implementar Validações
            var poll = Poll.MountInsert(requestInsertPoll.PollDescription, requestInsertPoll.Options);

            await _pollEFRepository.Insert(poll);

            return _customContractResolver
                .GetObjectIgnoringProperties(poll, "poll_description", "views", "options");
        }

        public async Task<object> GetById(int pollId)
        {
            // TODO: Atualizar views

            var poll = await _pollEFRepository.GetById(pollId);
            
            return _customContractResolver
                .GetObjectIgnoringProperties(MountListIgnorePropertiesToGetById(poll));
        }

        public async Task<Poll> GetStatsById(int pollId)
        {
            return await _pollEFRepository.GetStatsById(pollId);
        }

        private ListIgnoreProperties MountListIgnorePropertiesToGetById(Poll poll)
        {
            var listIgnoreProperties = new ListIgnoreProperties();

            listIgnoreProperties.AddProperty(poll, "views");
            listIgnoreProperties.AddProperty(poll?.Options.FirstOrDefault(), "poll_id", "votes");

            return listIgnoreProperties;
        }
    }
}