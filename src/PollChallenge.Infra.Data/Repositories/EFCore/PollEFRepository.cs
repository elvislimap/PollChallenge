using Microsoft.EntityFrameworkCore;
using PollChallenge.Domain.Entities;
using PollChallenge.Domain.Interfaces.Repositories.EFCore;
using PollChallenge.Domain.ValueObjects;
using PollChallenge.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
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

        public async Task UpdateViews(Poll poll)
        {
            if (poll == null) return;

            _context.Entry(poll).Property(p => p.Views).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task<Poll> GetById(int pollId)
        {
            return await _context.Polls
                .Include(p => p.Options)
                .FirstOrDefaultAsync(p => p.PollId == pollId);
        }

        public async Task<Stats> GetStatsById(int pollId)
        {
            return await _context.Polls
                .Join(_context.PollOptions, p => p.PollId, o => o.PollId, (p, o) => new { Poll = p, Option = o })
                .Where(po => po.Poll.PollId == pollId)
                .GroupBy(po =>
                    new
                    {
                        po.Poll.PollId,
                        po.Poll.Views,
                        po.Option.OptionId
                    })
                .GroupJoin(_context.Votes, g => new { g.Key.OptionId, g.Key.PollId }, v => new { v.OptionId, v.PollId },
                    (g, v) => new { GroupBy = g, Votes = v })
                .Select(s => new Stats(s.GroupBy.Key.Views,
                    new List<VoteStats> { new VoteStats(s.GroupBy.Key.OptionId, s.Votes.Count()) }))
                .GroupBy(s => s.Views)
                .Select(s => new Stats(s.Key, s.SelectMany(gs => gs.Votes)))
                .FirstOrDefaultAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}