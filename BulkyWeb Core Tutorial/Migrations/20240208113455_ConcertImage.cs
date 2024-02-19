using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MGTConcerts.Migrations
{
    /// <inheritdoc />
    public partial class ConcertImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Concerts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Concerts");
        }
    }
}
