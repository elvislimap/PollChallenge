using Microsoft.EntityFrameworkCore;
using PollChallenge.Domain.Entities;
using PollChallenge.Infra.Data.Mappings.EFCore;

namespace PollChallenge.Infra.Data.Contexts
{
    public sealed class ContextEf : DbContext
    {
        public ContextEf(DbContextOptions options) : base(options) { }

        public DbSet<Poll> Polls { get; }
        public DbSet<PollOption> PollOptions { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ForSqlServerUseIdentityColumns();
            modelBuilder.HasDefaultSchema("PollChallenge");

            modelBuilder.ApplyConfiguration(new PollMapping());
            modelBuilder.ApplyConfiguration(new PollOptionMapping());
        }
    }
}