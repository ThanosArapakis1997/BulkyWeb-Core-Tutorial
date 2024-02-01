using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MGTConcerts.Migrations
{
    /// <inheritdoc />
    public partial class ConcertName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConcertName",
                table: "Concerts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcertName",
                table: "Concerts");
        }
    }
}
