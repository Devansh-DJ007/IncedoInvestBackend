using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IncedoInvest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvestmentTypes",
                columns: table => new
                {
                    InvestmentTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvestmentTypeName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DeletedFlag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentTypes", x => x.InvestmentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentStrategies",
                columns: table => new
                {
                    InvestmentStrategyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvestmentStrategyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    InvestmentTypeId = table.Column<int>(type: "int", nullable: false),
                    Return10Y = table.Column<double>(type: "float", nullable: false),
                    Risk10Y = table.Column<double>(type: "float", nullable: false),
                    Return1Y = table.Column<double>(type: "float", nullable: false),
                    DeletedFlag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentStrategies", x => x.InvestmentStrategyId);
                    table.ForeignKey(
                        name: "FK_InvestmentStrategies_InvestmentTypes_InvestmentTypeId",
                        column: x => x.InvestmentTypeId,
                        principalTable: "InvestmentTypes",
                        principalColumn: "InvestmentTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    State = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Pincode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Company = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AdvisorId = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedFlag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentInfos",
                columns: table => new
                {
                    InvestmentInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    InvestmentAmount = table.Column<double>(type: "float", nullable: false),
                    InvestmentTypeId = table.Column<int>(type: "int", nullable: false),
                    Accepted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedFlag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentInfos", x => x.InvestmentInfoId);
                    table.ForeignKey(
                        name: "FK_InvestmentInfos_InvestmentTypes_InvestmentTypeId",
                        column: x => x.InvestmentTypeId,
                        principalTable: "InvestmentTypes",
                        principalColumn: "InvestmentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvestmentInfos_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProposedInvestments",
                columns: table => new
                {
                    PropesedInvestmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvestmentInfoId = table.Column<int>(type: "int", nullable: false),
                    InvestmentStrategyId = table.Column<int>(type: "int", nullable: false),
                    AcceptedFlag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposedInvestments", x => x.PropesedInvestmentId);
                    table.ForeignKey(
                        name: "FK_ProposedInvestments_InvestmentInfos_InvestmentInfoId",
                        column: x => x.InvestmentInfoId,
                        principalTable: "InvestmentInfos",
                        principalColumn: "InvestmentInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProposedInvestments_InvestmentStrategies_InvestmentStrategyId",
                        column: x => x.InvestmentStrategyId,
                        principalTable: "InvestmentStrategies",
                        principalColumn: "InvestmentStrategyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentInfos_InvestmentTypeId",
                table: "InvestmentInfos",
                column: "InvestmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentInfos_UserId",
                table: "InvestmentInfos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentStrategies_InvestmentTypeId",
                table: "InvestmentStrategies",
                column: "InvestmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProposedInvestments_InvestmentInfoId",
                table: "ProposedInvestments",
                column: "InvestmentInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProposedInvestments_InvestmentStrategyId",
                table: "ProposedInvestments",
                column: "InvestmentStrategyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProposedInvestments");

            migrationBuilder.DropTable(
                name: "InvestmentInfos");

            migrationBuilder.DropTable(
                name: "InvestmentStrategies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "InvestmentTypes");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
