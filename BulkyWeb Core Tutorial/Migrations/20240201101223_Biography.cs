using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MGTConcerts.Migrations
{
    /// <inheritdoc />
    public partial class Biography : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "Artists",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Biography",
                table: "Artists");
        }
    }
}
