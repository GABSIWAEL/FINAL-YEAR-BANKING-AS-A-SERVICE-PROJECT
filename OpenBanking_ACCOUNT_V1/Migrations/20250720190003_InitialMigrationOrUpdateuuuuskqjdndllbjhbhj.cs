using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenBanking_ACCOUNT_V1.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationOrUpdateuuuuskqjdndllbjhbhj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Bank_id",
                table: "agents",
                newName: "bank_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "bank_id",
                table: "agents",
                newName: "Bank_id");
        }
    }
}
