using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineMovieTicketingApplication.Migrations
{
    /// <inheritdoc />
    public partial class seatsintotable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Id", "Available", "MovieId", "ShowTime", "TheatreId" },
                values: new object[,]
                {
                    { "A1", true, 1, new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { "A10", true, 1, new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { "A11", true, 1, new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { "A12", true, 1, new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { "A13", true, 1, new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { "A14", true, 1, new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { "A15", true, 1, new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { "A16", true, 1, new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { "A2", true, 1, new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { "A3", true, 1, new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { "A4", true, 1, new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { "A5", true, 1, new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { "A6", true, 1, new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { "A7", true, 1, new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { "A8", true, 1, new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { "A9", true, 1, new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: "A1");

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: "A10");

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: "A11");

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: "A12");

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: "A13");

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: "A14");

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: "A15");

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: "A16");

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: "A2");

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: "A3");

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: "A4");

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: "A5");

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: "A6");

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: "A7");

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: "A8");

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: "A9");
        }
    }
}
