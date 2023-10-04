using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IncedoInvest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestmentStrategies_InvestorInfos_InvestorInfoID",
                table: "InvestmentStrategies");

            migrationBuilder.DropForeignKey(
                name: "FK_InvestorInfos_Users_UserID",
                table: "InvestorInfos");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_InvestmentStrategies_InvestorInfoID",
                table: "InvestmentStrategies");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "InvestorInfos");

            migrationBuilder.DropColumn(
                name: "InvestmentName",
                table: "InvestorInfos");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "InvestorInfos");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "InvestorInfos");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "InvestmentTypes");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "InvestmentTypes");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "InvestmentTypes");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "InvestmentStrategies");

            migrationBuilder.DropColumn(
                name: "InvestorInfoID",
                table: "InvestmentStrategies");

            migrationBuilder.DropColumn(
                name: "ModelAPLID",
                table: "InvestmentStrategies");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "InvestmentStrategies");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "InvestmentStrategies");

            migrationBuilder.RenameColumn(
                name: "InvestorInfoID",
                table: "InvestorInfos",
                newName: "InvestorInfoId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "InvestorInfos",
                newName: "InvestmentTypeId");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "InvestorInfos",
                newName: "Accepted");

            migrationBuilder.RenameIndex(
                name: "IX_InvestorInfos_UserID",
                table: "InvestorInfos",
                newName: "IX_InvestorInfos_InvestmentTypeId");

            migrationBuilder.RenameColumn(
                name: "InvestmentTypeID",
                table: "InvestmentTypes",
                newName: "InvestmentTypeId");

            migrationBuilder.RenameColumn(
                name: "InvestmentTypeID",
                table: "InvestmentStrategies",
                newName: "InvestmentTypeId");

            migrationBuilder.RenameColumn(
                name: "InvestmentStrategyID",
                table: "InvestmentStrategies",
                newName: "InvestmentStrategyId");

            migrationBuilder.RenameColumn(
                name: "StrategyName",
                table: "InvestmentStrategies",
                newName: "InvestmentStrategyName");

            migrationBuilder.RenameColumn(
                name: "InvestmentAmount",
                table: "InvestmentStrategies",
                newName: "Risk10Y");

            migrationBuilder.AddColumn<int>(
                name: "AdvisorId",
                table: "InvestorInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "InvestorInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "InvestmentAmount",
                table: "InvestorInfos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Return10Y",
                table: "InvestmentStrategies",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Return1Y",
                table: "InvestmentStrategies",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_InvestorInfos_AdvisorId",
                table: "InvestorInfos",
                column: "AdvisorId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestorInfos_ClientId",
                table: "InvestorInfos",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentStrategies_InvestmentTypeId",
                table: "InvestmentStrategies",
                column: "InvestmentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestmentStrategies_InvestmentTypes_InvestmentTypeId",
                table: "InvestmentStrategies",
                column: "InvestmentTypeId",
                principalTable: "InvestmentTypes",
                principalColumn: "InvestmentTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvestorInfos_Advisors_AdvisorId",
                table: "InvestorInfos",
                column: "AdvisorId",
                principalTable: "Advisors",
                principalColumn: "AdvisorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvestorInfos_Clients_ClientId",
                table: "InvestorInfos",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvestorInfos_InvestmentTypes_InvestmentTypeId",
                table: "InvestorInfos",
                column: "InvestmentTypeId",
                principalTable: "InvestmentTypes",
                principalColumn: "InvestmentTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestmentStrategies_InvestmentTypes_InvestmentTypeId",
                table: "InvestmentStrategies");

            migrationBuilder.DropForeignKey(
                name: "FK_InvestorInfos_Advisors_AdvisorId",
                table: "InvestorInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_InvestorInfos_Clients_ClientId",
                table: "InvestorInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_InvestorInfos_InvestmentTypes_InvestmentTypeId",
                table: "InvestorInfos");

            migrationBuilder.DropIndex(
                name: "IX_InvestorInfos_AdvisorId",
                table: "InvestorInfos");

            migrationBuilder.DropIndex(
                name: "IX_InvestorInfos_ClientId",
                table: "InvestorInfos");

            migrationBuilder.DropIndex(
                name: "IX_InvestmentStrategies_InvestmentTypeId",
                table: "InvestmentStrategies");

            migrationBuilder.DropColumn(
                name: "AdvisorId",
                table: "InvestorInfos");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "InvestorInfos");

            migrationBuilder.DropColumn(
                name: "InvestmentAmount",
                table: "InvestorInfos");

            migrationBuilder.DropColumn(
                name: "Return10Y",
                table: "InvestmentStrategies");

            migrationBuilder.DropColumn(
                name: "Return1Y",
                table: "InvestmentStrategies");

            migrationBuilder.RenameColumn(
                name: "InvestorInfoId",
                table: "InvestorInfos",
                newName: "InvestorInfoID");

            migrationBuilder.RenameColumn(
                name: "InvestmentTypeId",
                table: "InvestorInfos",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "Accepted",
                table: "InvestorInfos",
                newName: "Active");

            migrationBuilder.RenameIndex(
                name: "IX_InvestorInfos_InvestmentTypeId",
                table: "InvestorInfos",
                newName: "IX_InvestorInfos_UserID");

            migrationBuilder.RenameColumn(
                name: "InvestmentTypeId",
                table: "InvestmentTypes",
                newName: "InvestmentTypeID");

            migrationBuilder.RenameColumn(
                name: "InvestmentTypeId",
                table: "InvestmentStrategies",
                newName: "InvestmentTypeID");

            migrationBuilder.RenameColumn(
                name: "InvestmentStrategyId",
                table: "InvestmentStrategies",
                newName: "InvestmentStrategyID");

            migrationBuilder.RenameColumn(
                name: "Risk10Y",
                table: "InvestmentStrategies",
                newName: "InvestmentAmount");

            migrationBuilder.RenameColumn(
                name: "InvestmentStrategyName",
                table: "InvestmentStrategies",
                newName: "StrategyName");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "InvestorInfos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InvestmentName",
                table: "InvestorInfos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "InvestorInfos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "InvestorInfos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "InvestmentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "InvestmentTypes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "InvestmentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "InvestmentStrategies",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "InvestorInfoID",
                table: "InvestmentStrategies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ModelAPLID",
                table: "InvestmentStrategies",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "InvestmentStrategies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "InvestmentStrategies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AdvisorID = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    AgentID = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ClientID = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Company = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedFlag = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SortName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentStrategies_InvestorInfoID",
                table: "InvestmentStrategies",
                column: "InvestorInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestmentStrategies_InvestorInfos_InvestorInfoID",
                table: "InvestmentStrategies",
                column: "InvestorInfoID",
                principalTable: "InvestorInfos",
                principalColumn: "InvestorInfoID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvestorInfos_Users_UserID",
                table: "InvestorInfos",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
