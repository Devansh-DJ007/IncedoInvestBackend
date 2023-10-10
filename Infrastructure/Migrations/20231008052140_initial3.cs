using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IncedoInvest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestorInfos_InvestmentTypes_InvestmentTypeId",
                table: "InvestorInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_InvestorInfos_Users_UserId",
                table: "InvestorInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvestorInfos",
                table: "InvestorInfos");

            migrationBuilder.RenameTable(
                name: "InvestorInfos",
                newName: "InvestmentInfos");

            migrationBuilder.RenameColumn(
                name: "InvestorInfoId",
                table: "InvestmentInfos",
                newName: "InvestmentInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_InvestorInfos_UserId",
                table: "InvestmentInfos",
                newName: "IX_InvestmentInfos_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_InvestorInfos_InvestmentTypeId",
                table: "InvestmentInfos",
                newName: "IX_InvestmentInfos_InvestmentTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvestmentInfos",
                table: "InvestmentInfos",
                column: "InvestmentInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestmentInfos_InvestmentTypes_InvestmentTypeId",
                table: "InvestmentInfos",
                column: "InvestmentTypeId",
                principalTable: "InvestmentTypes",
                principalColumn: "InvestmentTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvestmentInfos_Users_UserId",
                table: "InvestmentInfos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestmentInfos_InvestmentTypes_InvestmentTypeId",
                table: "InvestmentInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_InvestmentInfos_Users_UserId",
                table: "InvestmentInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvestmentInfos",
                table: "InvestmentInfos");

            migrationBuilder.RenameTable(
                name: "InvestmentInfos",
                newName: "InvestorInfos");

            migrationBuilder.RenameColumn(
                name: "InvestmentInfoId",
                table: "InvestorInfos",
                newName: "InvestorInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_InvestmentInfos_UserId",
                table: "InvestorInfos",
                newName: "IX_InvestorInfos_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_InvestmentInfos_InvestmentTypeId",
                table: "InvestorInfos",
                newName: "IX_InvestorInfos_InvestmentTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvestorInfos",
                table: "InvestorInfos",
                column: "InvestorInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestorInfos_InvestmentTypes_InvestmentTypeId",
                table: "InvestorInfos",
                column: "InvestmentTypeId",
                principalTable: "InvestmentTypes",
                principalColumn: "InvestmentTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvestorInfos_Users_UserId",
                table: "InvestorInfos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
