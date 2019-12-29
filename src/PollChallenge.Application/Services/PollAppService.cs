using System.Threading.Tasks;
using PollChallenge.Application.Interfaces;
using PollChallenge.Domain.Entities;
using PollChallenge.Domain.Interfaces.Repositories.EFCore;
using PollChallenge.Domain.ValueObjects;

namespace PollChallenge.Application.Services
{
    public class PollAppService : IPollAppService
    {
        private readonly IPollEFRepository _pollEFRepository;

        public PollAppService(IPollEFRepository pollEFRepository)
        {
            _pollEFRepository = pollEFRepository;
        }


        public async Task<Poll> Insert(RequestInsertPoll requestInsertPoll)
        {
            // TODO: Implementar Validações
            var poll = Poll.MountInsert(requestInsertPoll.PollDescription, requestInsertPoll.Options);

            await _pollEFRepository.Insert(poll);
            return poll.GetOnlyPollId();
        }

        public async Task<Poll> GetById(int pollId)
        {
            // TODO: Atualizar views
            return await _pollEFRepository.GetById(pollId);
        }

        public async Task<Poll> GetStatsById(int pollId)
        {
            return await _pollEFRepository.GetStatsById(pollId);
        }
    }
}