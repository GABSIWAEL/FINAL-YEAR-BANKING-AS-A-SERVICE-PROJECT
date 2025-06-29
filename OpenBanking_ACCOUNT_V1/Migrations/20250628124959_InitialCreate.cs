using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenBanking_ACCOUNT_V1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountRoutings",
                columns: table => new
                {
                    Scheme = table.Column<int>(type: "integer", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRoutings", x => x.Scheme);
                });

            migrationBuilder.CreateTable(
                name: "Balance",
                columns: table => new
                {
                    currency = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balance", x => x.currency);
                });

            migrationBuilder.CreateTable(
                name: "ViewsAvailable",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    short_name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    is_public = table.Column<bool>(type: "boolean", nullable: false),
                    alias = table.Column<int>(type: "integer", nullable: false),
                    HideMetadataIfAliasUsed = table.Column<bool>(type: "boolean", nullable: false),
                    CanAddComment = table.Column<bool>(type: "boolean", nullable: false),
                    CanAddCorporateLocation = table.Column<bool>(type: "boolean", nullable: false),
                    CanAddImage = table.Column<bool>(type: "boolean", nullable: false),
                    CanAddImageUrl = table.Column<bool>(type: "boolean", nullable: false),
                    CanAddMoreInfo = table.Column<bool>(type: "boolean", nullable: false),
                    CanAddOpenCorporatesUrl = table.Column<bool>(type: "boolean", nullable: false),
                    CanAddPhysicalLocation = table.Column<bool>(type: "boolean", nullable: false),
                    CanAddPrivateAlias = table.Column<bool>(type: "boolean", nullable: false),
                    CanAddPublicAlias = table.Column<bool>(type: "boolean", nullable: false),
                    CanAddTag = table.Column<bool>(type: "boolean", nullable: false),
                    CanAddUrl = table.Column<bool>(type: "boolean", nullable: false),
                    CanAddWhereTag = table.Column<bool>(type: "boolean", nullable: false),
                    CanDeleteComment = table.Column<bool>(type: "boolean", nullable: false),
                    CanDeleteCorporateLocation = table.Column<bool>(type: "boolean", nullable: false),
                    CanDeleteImage = table.Column<bool>(type: "boolean", nullable: false),
                    CanDeletePhysicalLocation = table.Column<bool>(type: "boolean", nullable: false),
                    CanDeleteTag = table.Column<bool>(type: "boolean", nullable: false),
                    CanDeleteWhereTag = table.Column<bool>(type: "boolean", nullable: false),
                    CanEditOwnerComment = table.Column<bool>(type: "boolean", nullable: false),
                    CanSeeBankAccountBalance = table.Column<bool>(type: "boolean", nullable: false),
                    CanSeeBankAccountBankName = table.Column<bool>(type: "boolean", nullable: false),
                    CanSeeBankAccountCurrency = table.Column<bool>(type: "boolean", nullable: false),
                    CanSeeBankAccountIban = table.Column<bool>(type: "boolean", nullable: false),
                    CanSeeBankAccountLabel = table.Column<bool>(type: "boolean", nullable: false),
                    CanSeeBankAccountNationalIdentifier = table.Column<bool>(type: "boolean", nullable: false),
                    CanSeeBankAccountNumber = table.Column<bool>(type: "boolean", nullable: false),
                    CanSeeBankAccountOwners = table.Column<bool>(type: "boolean", nullable: false),
                    CanSeeBankAccountSwiftBic = table.Column<bool>(type: "boolean", nullable: false),
                    CanSeeBankAccountType = table.Column<bool>(type: "boolean", nullable: false),
                    CanSeeComments = table.Column<bool>(type: "boolean", nullable: false),
                    CanSeeCorporateLocation = table.Column<bool>(type: "boolean", nullable: false),
                    CanSeeImageUrl = table.Column<bool>(type: "boolean", nullable: false),
                    CanSeeImages = table.Column<bool>(type: "boolean", nullable: false),
                    CanSeeMoreInfo = table.Column<bool>(type: "boolean", nullable: false),
                    CanSeeOpenCorporatesUrl = table.Column<bool>(type: "boolean", nullable: false),
                    CanSeeOtherAccountBankName = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewsAvailable", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    label = table.Column<string>(type: "text", nullable: false),
                    number = table.Column<int>(type: "integer", nullable: false),
                    product_code = table.Column<string>(type: "text", nullable: false),
                    balancecurrency = table.Column<int>(type: "integer", nullable: false),
                    views_availableid = table.Column<string>(type: "text", nullable: true),
                    Bank_id = table.Column<string>(type: "text", nullable: false),
                    account_routingsScheme = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.id);
                    table.ForeignKey(
                        name: "FK_Accounts_AccountRoutings_account_routingsScheme",
                        column: x => x.account_routingsScheme,
                        principalTable: "AccountRoutings",
                        principalColumn: "Scheme",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Balance_balancecurrency",
                        column: x => x.balancecurrency,
                        principalTable: "Balance",
                        principalColumn: "currency",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_ViewsAvailable_views_availableid",
                        column: x => x.views_availableid,
                        principalTable: "ViewsAvailable",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AccountAttributes",
                columns: table => new
                {
                    account_attribute_id = table.Column<string>(type: "text", nullable: false),
                    product_code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false),
                    product_instance_code = table.Column<string>(type: "text", nullable: false),
                    Accountid = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountAttributes", x => x.account_attribute_id);
                    table.ForeignKey(
                        name: "FK_AccountAttributes_Accounts_Accountid",
                        column: x => x.Accountid,
                        principalTable: "Accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    provider = table.Column<string>(type: "text", nullable: false),
                    dispay_name = table.Column<string>(type: "text", nullable: false),
                    Accountid = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.id);
                    table.ForeignKey(
                        name: "FK_Owners_Accounts_Accountid",
                        column: x => x.Accountid,
                        principalTable: "Accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    userid = table.Column<string>(type: "text", nullable: false),
                    Accountid = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.id);
                    table.ForeignKey(
                        name: "FK_Tags_Accounts_Accountid",
                        column: x => x.Accountid,
                        principalTable: "Accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Tags_Owners_userid",
                        column: x => x.userid,
                        principalTable: "Owners",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountAttributes_Accountid",
                table: "AccountAttributes",
                column: "Accountid");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_account_routingsScheme",
                table: "Accounts",
                column: "account_routingsScheme");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_balancecurrency",
                table: "Accounts",
                column: "balancecurrency");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_views_availableid",
                table: "Accounts",
                column: "views_availableid");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_Accountid",
                table: "Owners",
                column: "Accountid");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Accountid",
                table: "Tags",
                column: "Accountid");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_userid",
                table: "Tags",
                column: "userid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountAttributes");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountRoutings");

            migrationBuilder.DropTable(
                name: "Balance");

            migrationBuilder.DropTable(
                name: "ViewsAvailable");
        }
    }
}
