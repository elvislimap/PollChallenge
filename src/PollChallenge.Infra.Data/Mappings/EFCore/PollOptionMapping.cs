using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PollChallenge.Domain.Entities;

namespace PollChallenge.Infra.Data.Mappings.EFCore
{
    public class PollOptionMapping : IEntityTypeConfiguration<PollOption>
    {
        public void Configure(EntityTypeBuilder<PollOption> builder)
        {
            builder.HasKey(p => new { p.PollOptionId, p.PollId });

            builder.Property(p => p.PollOptionId).HasColumnType("int");
            builder.Property(p => p.PollId).HasColumnType("int").IsRequired();
            builder.Property(p => p.Description).HasColumnType("varchar(50)").IsRequired();

            builder.HasOne(po => po.Poll).WithMany(p => p.Options);
        }
    }
}