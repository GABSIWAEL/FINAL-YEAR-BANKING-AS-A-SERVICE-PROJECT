using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OpenBanking_ACCOUNT_V1.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountAttributes_Accounts_Accountid",
                table: "AccountAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoutings_Accounts_Accountid",
                table: "AccountRoutings");

            migrationBuilder.DropForeignKey(
                name: "FK_Balance_Accounts_Accountid",
                table: "Balance");

            migrationBuilder.DropForeignKey(
                name: "FK_Owners_Accounts_Accountid",
                table: "Owners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Balance",
                table: "Balance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountRoutings",
                table: "AccountRoutings");

            migrationBuilder.AlterColumn<string>(
                name: "Accountid",
                table: "Owners",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Accountid",
                table: "Balance",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BalanceId",
                table: "Balance",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Accountid",
                table: "AccountRoutings",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoutingId",
                table: "AccountRoutings",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Accountid",
                table: "AccountAttributes",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Balance",
                table: "Balance",
                column: "BalanceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountRoutings",
                table: "AccountRoutings",
                column: "RoutingId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountAttributes_Accounts_Accountid",
                table: "AccountAttributes",
                column: "Accountid",
                principalTable: "Accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoutings_Accounts_Accountid",
                table: "AccountRoutings",
                column: "Accountid",
                principalTable: "Accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Balance_Accounts_Accountid",
                table: "Balance",
                column: "Accountid",
                principalTable: "Accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_Accounts_Accountid",
                table: "Owners",
                column: "Accountid",
                principalTable: "Accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountAttributes_Accounts_Accountid",
                table: "AccountAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoutings_Accounts_Accountid",
                table: "AccountRoutings");

            migrationBuilder.DropForeignKey(
                name: "FK_Balance_Accounts_Accountid",
                table: "Balance");

            migrationBuilder.DropForeignKey(
                name: "FK_Owners_Accounts_Accountid",
                table: "Owners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Balance",
                table: "Balance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountRoutings",
                table: "AccountRoutings");

            migrationBuilder.DropColumn(
                name: "BalanceId",
                table: "Balance");

            migrationBuilder.DropColumn(
                name: "RoutingId",
                table: "AccountRoutings");

            migrationBuilder.AlterColumn<string>(
                name: "Accountid",
                table: "Owners",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Accountid",
                table: "Balance",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Accountid",
                table: "AccountRoutings",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Accountid",
                table: "AccountAttributes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Balance",
                table: "Balance",
                column: "currency");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountRoutings",
                table: "AccountRoutings",
                column: "Scheme");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountAttributes_Accounts_Accountid",
                table: "AccountAttributes",
                column: "Accountid",
                principalTable: "Accounts",
                principalColumn: "id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_Accounts_Accountid",
                table: "Owners",
                column: "Accountid",
                principalTable: "Accounts",
                principalColumn: "id");
        }
    }
}
