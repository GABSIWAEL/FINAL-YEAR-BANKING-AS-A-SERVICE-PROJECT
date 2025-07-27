using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenBanking_ACCOUNT_V1.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationOrUpdateuuuuskqjdnd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AccountRoutings_account_routingsScheme",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Balance_balancecurrency",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_account_routingsScheme",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_balancecurrency",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "account_routingsScheme",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "balancecurrency",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "Accountid",
                table: "Balance",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Accountid",
                table: "AccountRoutings",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Balance_Accountid",
                table: "Balance",
                column: "Accountid");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRoutings_Accountid",
                table: "AccountRoutings",
                column: "Accountid");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoutings_Accounts_Accountid",
                table: "AccountRoutings",
                column: "Accountid",
                principalTable: "Accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Balance_Accounts_Accountid",
                table: "Balance",
                column: "Accountid",
                principalTable: "Accounts",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoutings_Accounts_Accountid",
                table: "AccountRoutings");

            migrationBuilder.DropForeignKey(
                name: "FK_Balance_Accounts_Accountid",
                table: "Balance");

            migrationBuilder.DropIndex(
                name: "IX_Balance_Accountid",
                table: "Balance");

            migrationBuilder.DropIndex(
                name: "IX_AccountRoutings_Accountid",
                table: "AccountRoutings");

            migrationBuilder.DropColumn(
                name: "Accountid",
                table: "Balance");

            migrationBuilder.DropColumn(
                name: "Accountid",
                table: "AccountRoutings");

            migrationBuilder.AddColumn<int>(
                name: "account_routingsScheme",
                table: "Accounts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "balancecurrency",
                table: "Accounts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_account_routingsScheme",
                table: "Accounts",
                column: "account_routingsScheme");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_balancecurrency",
                table: "Accounts",
                column: "balancecurrency");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AccountRoutings_account_routingsScheme",
                table: "Accounts",
                column: "account_routingsScheme",
                principalTable: "AccountRoutings",
                principalColumn: "Scheme",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Balance_balancecurrency",
                table: "Accounts",
                column: "balancecurrency",
                principalTable: "Balance",
                principalColumn: "currency",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
