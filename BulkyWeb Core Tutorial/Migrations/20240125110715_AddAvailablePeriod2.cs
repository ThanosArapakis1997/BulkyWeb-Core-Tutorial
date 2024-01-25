using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulkyWeb_Core_Tutorial.Migrations
{
    /// <inheritdoc />
    public partial class AddAvailablePeriod2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AvailableTo",
                table: "Music_Venues",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AvailableFrom",
                table: "Music_Venues",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Music_Venues",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AvailableFrom", "AvailableTo" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Music_Venues",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AvailableFrom", "AvailableTo" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Music_Venues",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AvailableFrom", "AvailableTo" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AvailableTo",
                table: "Music_Venues",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AvailableFrom",
                table: "Music_Venues",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Music_Venues",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AvailableFrom", "AvailableTo" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Music_Venues",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AvailableFrom", "AvailableTo" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Music_Venues",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AvailableFrom", "AvailableTo" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
