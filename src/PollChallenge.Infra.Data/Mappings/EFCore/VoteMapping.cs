using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PollChallenge.Domain.Entities;

namespace PollChallenge.Infra.Data.Mappings.EFCore
{
    public class VoteMapping : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.HasKey(v => v.VoteId);

            builder.Property(v => v.VoteId).HasColumnType("int").ValueGeneratedOnAdd();
            builder.Property(v => v.PollId).HasColumnType("int").IsRequired();
            builder.Property(v => v.PollOptionId).HasColumnType("int").IsRequired();

            builder.HasOne(v => v.PollOption).WithMany(p => p.Votes);
        }
    }
}