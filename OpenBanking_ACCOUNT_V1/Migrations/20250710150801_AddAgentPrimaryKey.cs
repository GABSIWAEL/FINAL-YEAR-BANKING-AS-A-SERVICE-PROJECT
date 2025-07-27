using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenBanking_ACCOUNT_V1.Migrations
{
    /// <inheritdoc />
    public partial class AddAgentPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "agents",
                columns: table => new
                {
                    agent_id = table.Column<string>(type: "text", nullable: false),
                    Bank_id = table.Column<string>(type: "text", nullable: false),
                    legal_name = table.Column<string>(type: "text", nullable: false),
                    mobile_phone_number = table.Column<string>(type: "text", nullable: false),
                    agent_number = table.Column<string>(type: "text", nullable: false),
                    currency = table.Column<int>(type: "integer", nullable: false),
                    is_confirmed_agent = table.Column<bool>(type: "boolean", nullable: false),
                    is_pending_agent = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agents", x => x.agent_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agents");
        }
    }
}
