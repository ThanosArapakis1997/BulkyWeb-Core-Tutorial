using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MGTConcerts.Migrations
{
    /// <inheritdoc />
    public partial class PresentField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Present",
                table: "Orders",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Present",
                table: "Orders");
        }
    }
}
