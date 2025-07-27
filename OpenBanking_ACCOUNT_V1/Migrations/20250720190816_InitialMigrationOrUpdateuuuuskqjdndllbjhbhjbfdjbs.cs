using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenBanking_ACCOUNT_V1.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationOrUpdateuuuuskqjdndllbjhbhjbfdjbs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_agents",
                table: "agents");

            migrationBuilder.RenameTable(
                name: "agents",
                newName: "Agent");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agent",
                table: "Agent",
                column: "agent_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Agent",
                table: "Agent");

            migrationBuilder.RenameTable(
                name: "Agent",
                newName: "agents");

            migrationBuilder.AddPrimaryKey(
                name: "PK_agents",
                table: "agents",
                column: "agent_id");
        }
    }
}
