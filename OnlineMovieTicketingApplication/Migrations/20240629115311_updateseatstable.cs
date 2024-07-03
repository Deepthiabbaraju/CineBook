using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineMovieTicketingApplication.Migrations
{
    /// <inheritdoc />
    public partial class updateseatstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Seats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ShowTime",
                table: "Seats",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TheatreId",
                table: "Seats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "ShowTime",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "TheatreId",
                table: "Seats");
        }
    }
}
