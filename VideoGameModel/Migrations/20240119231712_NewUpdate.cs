using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoGameModel.Migrations
{
    /// <inheritdoc />
    public partial class NewUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_VideoGames_VideoGameId",
                table: "Genres");

            migrationBuilder.DropForeignKey(
                name: "FK_Platforms_VideoGames_VideoGameId",
                table: "Platforms");

            migrationBuilder.DropIndex(
                name: "IX_Platforms_VideoGameId",
                table: "Platforms");

            migrationBuilder.DropIndex(
                name: "IX_Genres_VideoGameId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "VideoGameId",
                table: "Platforms");

            migrationBuilder.DropColumn(
                name: "VideoGameId",
                table: "Genres");

            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "VideoGames",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlatformId",
                table: "VideoGames",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VideoGames_GenreId",
                table: "VideoGames",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoGames_PlatformId",
                table: "VideoGames",
                column: "PlatformId");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoGames_Genres_GenreId",
                table: "VideoGames",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoGames_Platforms_PlatformId",
                table: "VideoGames",
                column: "PlatformId",
                principalTable: "Platforms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoGames_Genres_GenreId",
                table: "VideoGames");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoGames_Platforms_PlatformId",
                table: "VideoGames");

            migrationBuilder.DropIndex(
                name: "IX_VideoGames_GenreId",
                table: "VideoGames");

            migrationBuilder.DropIndex(
                name: "IX_VideoGames_PlatformId",
                table: "VideoGames");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "VideoGames");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "VideoGames");

            migrationBuilder.AddColumn<int>(
                name: "VideoGameId",
                table: "Platforms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VideoGameId",
                table: "Genres",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Platforms_VideoGameId",
                table: "Platforms",
                column: "VideoGameId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_VideoGameId",
                table: "Genres",
                column: "VideoGameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_VideoGames_VideoGameId",
                table: "Genres",
                column: "VideoGameId",
                principalTable: "VideoGames",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Platforms_VideoGames_VideoGameId",
                table: "Platforms",
                column: "VideoGameId",
                principalTable: "VideoGames",
                principalColumn: "Id");
        }
    }
}
