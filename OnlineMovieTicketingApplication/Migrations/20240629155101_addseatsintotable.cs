using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineMovieTicketingApplication.Migrations
{
    /// <inheritdoc />
    public partial class addseatsintotable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: "A1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Id", "Available", "MovieId", "ShowTime", "TheatreId" },
                values: new object[] { "A1", true, 1, new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 });
        }
    }
}
