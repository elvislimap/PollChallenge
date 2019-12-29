using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PollChallenge.Domain.Entities;

namespace PollChallenge.Infra.Data.Mappings.EFCore
{
    public class PollMapping : IEntityTypeConfiguration<Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            builder.HasKey(p => p.PollId);

            builder.Property(p => p.PollId).HasColumnType("int").ValueGeneratedOnAdd();
            builder.Property(p => p.PollDescription).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.Views).HasColumnType("int").IsRequired(false);
        }
    }
}