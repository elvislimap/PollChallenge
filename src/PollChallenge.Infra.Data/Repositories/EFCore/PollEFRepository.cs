using Microsoft.EntityFrameworkCore;
using PollChallenge.Domain.Entities;
using PollChallenge.Domain.Interfaces.Repositories.EFCore;
using PollChallenge.Domain.ValueObjects;
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

        public async Task UpdateViews(Poll poll)
        {
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
            var listStats = await _context.Polls
                .Join(_context.Votes, p => p.PollId, v => v.PollId, (p, v) => new { Poll = p, Vote = v })
                .Where(j => j.Poll.PollId == pollId)
                .GroupBy(j => new { j.Poll.Views, j.Vote.OptionId })
                .Select(g => new Stats(g.Key.Views, g.Key.OptionId, g.Count()))
                .ToListAsync();

            var votesGrouped = listStats.SelectMany(s => s.Votes)
                .GroupBy(v => v.OptionId)
                .SelectMany(g => g.ToList(), (g, v) => new VoteStats(g.Key, v.Qty))
                .ToList();

            return new Stats(listStats.First().Views, votesGrouped);
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}