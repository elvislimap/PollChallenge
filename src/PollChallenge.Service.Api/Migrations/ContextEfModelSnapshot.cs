﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PollChallenge.Infra.Data.Contexts;

namespace PollChallenge.Service.Api.Migrations
{
    [DbContext(typeof(ContextEf))]
    partial class ContextEfModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("PollChallenge")
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PollChallenge.Domain.Entities.Poll", b =>
                {
                    b.Property<int>("PollId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PollDescription")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Views")
                        .HasColumnType("int");

                    b.HasKey("PollId");

                    b.ToTable("Polls");
                });

            modelBuilder.Entity("PollChallenge.Domain.Entities.PollOption", b =>
                {
                    b.Property<int>("OptionId")
                        .HasColumnType("int");

                    b.Property<int>("PollId")
                        .HasColumnType("int");

                    b.Property<string>("OptionDescription")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("OptionId", "PollId");

                    b.HasIndex("PollId");

                    b.ToTable("PollOptions");
                });

            modelBuilder.Entity("PollChallenge.Domain.Entities.Vote", b =>
                {
                    b.Property<int>("VoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OptionId")
                        .HasColumnType("int");

                    b.Property<int>("PollId")
                        .HasColumnType("int");

                    b.HasKey("VoteId");

                    b.HasIndex("OptionId", "PollId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("PollChallenge.Domain.Entities.PollOption", b =>
                {
                    b.HasOne("PollChallenge.Domain.Entities.Poll", "Poll")
                        .WithMany("Options")
                        .HasForeignKey("PollId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PollChallenge.Domain.Entities.Vote", b =>
                {
                    b.HasOne("PollChallenge.Domain.Entities.PollOption", "PollOption")
                        .WithMany("Votes")
                        .HasForeignKey("OptionId", "PollId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
