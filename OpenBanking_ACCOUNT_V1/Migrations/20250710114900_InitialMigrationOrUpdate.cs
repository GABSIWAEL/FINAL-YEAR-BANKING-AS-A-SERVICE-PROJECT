using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenBanking_ACCOUNT_V1.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationOrUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_ViewsAvailable_views_availableid",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_views_availableid",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "views_availableid",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "Accountid",
                table: "ViewsAvailable",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ViewsAvailable_Accountid",
                table: "ViewsAvailable",
                column: "Accountid");

            migrationBuilder.AddForeignKey(
                name: "FK_ViewsAvailable_Accounts_Accountid",
                table: "ViewsAvailable",
                column: "Accountid",
                principalTable: "Accounts",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ViewsAvailable_Accounts_Accountid",
                table: "ViewsAvailable");

            migrationBuilder.DropIndex(
                name: "IX_ViewsAvailable_Accountid",
                table: "ViewsAvailable");

            migrationBuilder.DropColumn(
                name: "Accountid",
                table: "ViewsAvailable");

            migrationBuilder.AddColumn<string>(
                name: "views_availableid",
                table: "Accounts",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_views_availableid",
                table: "Accounts",
                column: "views_availableid");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_ViewsAvailable_views_availableid",
                table: "Accounts",
                column: "views_availableid",
                principalTable: "ViewsAvailable",
                principalColumn: "id");
        }
    }
}
