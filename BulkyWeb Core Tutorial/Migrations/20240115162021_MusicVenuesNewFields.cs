using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulkyWeb_Core_Tutorial.Migrations
{
    /// <inheritdoc />
    public partial class MusicVenuesNewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Music_Venues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Music_Venues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Music_Venues",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Capacity", "Location" },
                values: new object[] { 5000, "Κεραμεικός" });

            migrationBuilder.UpdateData(
                table: "Music_Venues",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Capacity", "Location" },
                values: new object[] { 2000, "Ταύρος" });

            migrationBuilder.UpdateData(
                table: "Music_Venues",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Capacity", "Location" },
                values: new object[] { 2000, "Πειραιάς" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Music_Venues");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Music_Venues");
        }
    }
}
