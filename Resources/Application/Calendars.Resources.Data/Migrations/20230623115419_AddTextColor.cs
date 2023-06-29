using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calendars.Resources.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTextColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ArgbColorInteger",
                table: "Days",
                newName: "TextArgbColorInteger");

            migrationBuilder.AddColumn<int>(
                name: "BackgroundArgbColorInteger",
                table: "Days",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundArgbColorInteger",
                table: "Days");

            migrationBuilder.RenameColumn(
                name: "TextArgbColorInteger",
                table: "Days",
                newName: "ArgbColorInteger");
        }
    }
}
