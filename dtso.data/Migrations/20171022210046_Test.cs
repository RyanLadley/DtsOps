using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace dtso.data.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CityAccounts",
                columns: table => new
                {
                    CityAccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityAccounts", x => x.CityAccountId);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceTypes",
                columns: table => new
                {
                    InvoiceTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceTypes", x => x.InvoiceTypeId);
                });

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
                name: "vAccounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountNumber = table.Column<int>(type: "int", nullable: false),
                    AccountPrefix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnualBudget = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FundNumber = table.Column<int>(type: "int", nullable: false),
                    ProjectDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionalAccountCodeId = table.Column<int>(type: "int", nullable: false),
                    ShredNo = table.Column<int>(type: "int", nullable: true),
                    SubNo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vAccounts", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    VendorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ContractEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContractNumber = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    ContractStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    PointOfContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.VendorId);
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

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatePaid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    InvoiceTypeId = table.Column<int>(type: "int", nullable: false),
                    VendorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_InvoiceTypes_InvoiceTypeId",
                        column: x => x.InvoiceTypeId,
                        principalTable: "InvoiceTypes",
                        principalColumn: "InvoiceTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceAccounts",
                columns: table => new
                {
                    InvoiceAccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Expense = table.Column<decimal>(type: "Money", nullable: false, defaultValue: 0m),
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceAccounts", x => x.InvoiceAccountId);
                    table.ForeignKey(
                        name: "FK_InvoiceAccounts_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceAccounts_vAccounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "vAccounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceAccounts_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CityExpenses",
                columns: table => new
                {
                    CityExpenseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    CityAccountId = table.Column<int>(type: "int", nullable: false),
                    Expense = table.Column<decimal>(type: "Money", nullable: false, defaultValue: 0m),
                    InvoiceAccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityExpenses", x => x.CityExpenseId);
                    table.ForeignKey(
                        name: "FK_CityExpenses_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CityExpenses_CityAccounts_CityAccountId",
                        column: x => x.CityAccountId,
                        principalTable: "CityAccounts",
                        principalColumn: "CityAccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CityExpenses_InvoiceAccounts_InvoiceAccountId",
                        column: x => x.InvoiceAccountId,
                        principalTable: "InvoiceAccounts",
                        principalColumn: "InvoiceAccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RegionalAccountCodeId_SubNo_ShredNo",
                table: "Accounts",
                columns: new[] { "RegionalAccountCodeId", "SubNo", "ShredNo" },
                unique: true,
                filter: "[SubNo] IS NOT NULL AND [ShredNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CityExpenses_AccountId",
                table: "CityExpenses",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CityExpenses_InvoiceAccountId",
                table: "CityExpenses",
                column: "InvoiceAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CityExpenses_CityAccountId_InvoiceAccountId",
                table: "CityExpenses",
                columns: new[] { "CityAccountId", "InvoiceAccountId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceAccounts_AccountId",
                table: "InvoiceAccounts",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceAccounts_InvoiceId_AccountId",
                table: "InvoiceAccounts",
                columns: new[] { "InvoiceId", "AccountId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceTypeId",
                table: "Invoices",
                column: "InvoiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_VendorId_InvoiceNumber",
                table: "Invoices",
                columns: new[] { "VendorId", "InvoiceNumber" },
                unique: true,
                filter: "[InvoiceNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_vAccounts_RegionalAccountCodeId_SubNo_ShredNo",
                table: "vAccounts",
                columns: new[] { "RegionalAccountCodeId", "SubNo", "ShredNo" },
                unique: true,
                filter: "[SubNo] IS NOT NULL AND [ShredNo] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityExpenses");

            migrationBuilder.DropTable(
                name: "CityAccounts");

            migrationBuilder.DropTable(
                name: "InvoiceAccounts");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "vAccounts");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "RegionalAccountCodes");

            migrationBuilder.DropTable(
                name: "InvoiceTypes");

            migrationBuilder.DropTable(
                name: "Vendors");
        }
    }
}
