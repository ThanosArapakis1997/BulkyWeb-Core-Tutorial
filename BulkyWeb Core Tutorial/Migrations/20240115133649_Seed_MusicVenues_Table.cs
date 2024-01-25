using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MGTConcerts.Migrations
{
    /// <inheritdoc />
    public partial class Seed_MusicVenues_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Music_Venues",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Τεχνόπολη Δήμου Αθηναίων", "Τεχνοπολις" },
                    { 2, "Music Club", "Fuzz" },
                    { 3, "Συναυλιακός Χώρος Δήμου Πειραιά", "Λιπάσματα" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Music_Venues",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Music_Venues",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Music_Venues",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
