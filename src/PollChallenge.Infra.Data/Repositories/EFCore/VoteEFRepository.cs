using PollChallenge.Domain.Entities;
using PollChallenge.Domain.Interfaces.Repositories.EFCore;
using PollChallenge.Infra.Data.Contexts;
using System;
using System.Threading.Tasks;

namespace PollChallenge.Infra.Data.Repositories.EFCore
{
    public sealed class VoteEFRepository : IVoteEFRepository
    {
        private readonly ContextEf _context;

        public VoteEFRepository(ContextEf context)
        {
            _context = context;
        }


        public async Task Insert(Vote vote)
        {
            _context.Add(vote);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}