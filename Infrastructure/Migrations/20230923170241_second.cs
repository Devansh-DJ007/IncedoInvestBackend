using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IncedoInvest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Advisors");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Advisors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Advisors");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Advisors",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "");
        }
    }
}
