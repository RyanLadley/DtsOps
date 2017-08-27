using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace dtso.data.Migrations
{
    public partial class InitialAccounts : Migration
    {
        private string _sqlAccountsView =
            "CREATE VIEW vAccounts AS " +
            "SELECT  Accounts.AccountId AS AccountId, " +
            "	     Accounts.RegionalAccountCodeId AS RegionalAccountCodeId, " +
            "        Regional.AccountNumber AS AccountNumber, " +
            "        Accounts.SubNo AS SubNo, " +
            "        Accounts.ShredNo AS ShredNo, " +
            "        Accounts.Description AS Description, " +
            "        Accounts.AnnualBudget AS AnnualBudget, " +
            "        Regional.FundNumber AS FundNumber, " +
            "        Regional.ProjectNumber AS ProjectNumber, " +
            "        Regional.ProjectDescription AS ProjectDescription, " +
            "        Regional.AccountPrefix AS AccountPrefix " +
            "    FROM Accounts" +
            "    JOIN RegionalAccountCodes AS Regional ON Regional.RegionalAccountCodeId = Accounts.RegionalAccountCodeId";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegionalAccountCodes",
                columns: table => new
                {
                    RegionalAccountCodeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountNumber = table.Column<int>(type: "int", nullable: false),
                    AccountPrefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeptartmentNumber = table.Column<int>(type: "int", nullable: false),
                    FundNumber = table.Column<int>(type: "int", nullable: false),
                    ProjectDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionalAccountCodes", x => x.RegionalAccountCodeId);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnnualBudget = table.Column<decimal>(type: "Money", nullable: false, defaultValue: 0m),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionalAccountCodeId = table.Column<int>(type: "int", nullable: false),
                    ShredNo = table.Column<int>(type: "int", nullable: true, defaultValue: -1),
                    SubNo = table.Column<int>(type: "int", nullable: true, defaultValue: -1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounts_RegionalAccountCodes_RegionalAccountCodeId",
                        column: x => x.RegionalAccountCodeId,
                        principalTable: "RegionalAccountCodes",
                        principalColumn: "RegionalAccountCodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RegionalAccountCodeId_SubNo_ShredNo",
                table: "Accounts",
                columns: new[] { "RegionalAccountCodeId", "SubNo", "ShredNo" },
                unique: true,
                filter: "[SubNo] IS NOT NULL AND [ShredNo] IS NOT NULL");

            migrationBuilder.Sql("DROP VIEW IF EXISTS vAccounts");
            migrationBuilder.Sql(_sqlAccountsView);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "RegionalAccountCodes");
        }
    }
}
