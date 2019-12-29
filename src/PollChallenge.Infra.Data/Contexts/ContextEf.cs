using Microsoft.EntityFrameworkCore;
using PollChallenge.Domain.Entities;
using PollChallenge.Infra.Data.Mappings.EFCore;

namespace PollChallenge.Infra.Data.Contexts
{
    public sealed class ContextEf : DbContext
    {
        public ContextEf(DbContextOptions options) : base(options) { }

        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollOption> PollOptions { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ForSqlServerUseIdentityColumns();
            modelBuilder.HasDefaultSchema("PollChallenge");

            modelBuilder.ApplyConfiguration(new PollMapping());
            modelBuilder.ApplyConfiguration(new PollOptionMapping());
            modelBuilder.ApplyConfiguration(new VoteMapping());
        }
    }
}