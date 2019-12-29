using PollChallenge.Domain.Entities;
using PollChallenge.Domain.Interfaces.Repositories.EFCore;
using PollChallenge.Infra.Data.Contexts;
using System;
using System.Threading.Tasks;

namespace PollChallenge.Infra.Data.Repositories.EFCore
{
    public sealed class PollOptionEFRepository : IPollOptionEFRepository
    {
        private readonly ContextEf _context;

        public PollOptionEFRepository(ContextEf context)
        {
            _context = context;
        }


        public async Task<PollOption> GetByKey(int optionId, int pollId)
        {
            return await _context.PollOptions.FindAsync(optionId, pollId);
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}