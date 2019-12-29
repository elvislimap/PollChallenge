using Microsoft.EntityFrameworkCore;
using PollChallenge.Domain.Entities;
using PollChallenge.Domain.Interfaces.Repositories.EFCore;
using PollChallenge.Infra.Data.Contexts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PollChallenge.Infra.Data.Repositories.EFCore
{
    public sealed class PollEFRepository : IPollEFRepository
    {
        private readonly ContextEf _context;

        public PollEFRepository(ContextEf context)
        {
            _context = context;
        }


        public async Task Insert(Poll poll)
        {
            _context.Add(poll);
            await _context.SaveChangesAsync();
        }

        public async Task<Poll> GetById(int pollId)
        {
            return await _context.Polls.FindAsync(pollId);
        }

        public async Task<Poll> GetStatsById(int pollId)
        {
            // TODO: Implementar...
            var temp = await _context.Polls
                .Include(p => p.Options)
                .Where(p => p.PollId == pollId)
                .ToListAsync();

            return null;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}