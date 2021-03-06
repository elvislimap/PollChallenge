﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PollChallenge.Service.Api.Migrations
{
    public partial class MigrationInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "PollChallenge");

            migrationBuilder.CreateTable(
                name: "Polls",
                schema: "PollChallenge",
                columns: table => new
                {
                    PollId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PollDescription = table.Column<string>(type: "varchar(100)", nullable: false),
                    Views = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polls", x => x.PollId);
                });

            migrationBuilder.CreateTable(
                name: "PollOptions",
                schema: "PollChallenge",
                columns: table => new
                {
                    OptionId = table.Column<int>(type: "int", nullable: false),
                    PollId = table.Column<int>(type: "int", nullable: false),
                    OptionDescription = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollOptions", x => new { x.OptionId, x.PollId });
                    table.ForeignKey(
                        name: "FK_PollOptions_Polls_PollId",
                        column: x => x.PollId,
                        principalSchema: "PollChallenge",
                        principalTable: "Polls",
                        principalColumn: "PollId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                schema: "PollChallenge",
                columns: table => new
                {
                    VoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PollId = table.Column<int>(type: "int", nullable: false),
                    OptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.VoteId);
                    table.ForeignKey(
                        name: "FK_Votes_PollOptions_OptionId_PollId",
                        columns: x => new { x.OptionId, x.PollId },
                        principalSchema: "PollChallenge",
                        principalTable: "PollOptions",
                        principalColumns: new[] { "OptionId", "PollId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PollOptions_PollId",
                schema: "PollChallenge",
                table: "PollOptions",
                column: "PollId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_OptionId_PollId",
                schema: "PollChallenge",
                table: "Votes",
                columns: new[] { "OptionId", "PollId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Votes",
                schema: "PollChallenge");

            migrationBuilder.DropTable(
                name: "PollOptions",
                schema: "PollChallenge");

            migrationBuilder.DropTable(
                name: "Polls",
                schema: "PollChallenge");
        }
    }
}
