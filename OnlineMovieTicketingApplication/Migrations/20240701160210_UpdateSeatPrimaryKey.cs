using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineMovieTicketingApplication.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeatPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Seats",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_Seats_TheatreId",
                table: "Seats");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seats",
                table: "Seats",
                columns: new[] { "TheatreId", "ShowTime", "Id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Seats",
                table: "Seats");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seats",
                table: "Seats",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_Seats_TheatreId",
                table: "Seats",
                column: "TheatreId");
        }
    }
}
