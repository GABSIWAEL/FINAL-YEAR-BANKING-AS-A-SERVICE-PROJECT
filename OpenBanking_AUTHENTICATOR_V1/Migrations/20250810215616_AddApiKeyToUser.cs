using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenBanking_AUTHENTICATOR_V1.Migrations
{
    /// <inheritdoc />
    public partial class AddApiKeyToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApiKey",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApiKeyExpiry",
                table: "Users",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiKey",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ApiKeyExpiry",
                table: "Users");
        }
    }
}
