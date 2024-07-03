using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineMovieTicketingApplication.Migrations
{
    /// <inheritdoc />
    public partial class upadteseats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Seats_MovieId",
                table: "Seats",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_TheatreId",
                table: "Seats",
                column: "TheatreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Movies_MovieId",
                table: "Seats",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Theatres_TheatreId",
                table: "Seats",
                column: "TheatreId",
                principalTable: "Theatres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Movies_MovieId",
                table: "Seats");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Theatres_TheatreId",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_Seats_MovieId",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_Seats_TheatreId",
                table: "Seats");
        }
    }
}
