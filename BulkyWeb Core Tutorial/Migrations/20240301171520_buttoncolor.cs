using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MGTConcerts.Migrations
{
    /// <inheritdoc />
    public partial class buttoncolor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ButtonColor",
                table: "Concerts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ButtonColor",
                table: "Concerts");
        }
    }
}
