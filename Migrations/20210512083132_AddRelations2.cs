using Microsoft.EntityFrameworkCore.Migrations;

namespace P14MovieTheatre.Migrations
{
    public partial class AddRelations2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SessionId",
                table: "Tickets",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_MovieId",
                table: "Sessions",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Movies_MovieId",
                table: "Sessions",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Sessions_SessionId",
                table: "Tickets",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Movies_MovieId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Sessions_SessionId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_SessionId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_MovieId",
                table: "Sessions");
        }
    }
}
