using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarWars_App.Migrations
{
    public partial class add_film_character : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CharacterFilms",
                columns: new[] { "CharacterId", "FilmId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "CharacterFilms",
                columns: new[] { "CharacterId", "FilmId" },
                values: new object[] { 1, 3 });

            migrationBuilder.InsertData(
                table: "CharacterFilms",
                columns: new[] { "CharacterId", "FilmId" },
                values: new object[] { 1, 4 });

            migrationBuilder.InsertData(
                table: "CharacterFilms",
                columns: new[] { "CharacterId", "FilmId" },
                values: new object[] { 1, 6 });

            migrationBuilder.InsertData(
                table: "CharacterFilms",
                columns: new[] { "CharacterId", "FilmId" },
                values: new object[] { 1, 7 });

            migrationBuilder.InsertData(
                table: "CharacterFilms",
                columns: new[] { "CharacterId", "FilmId" },
                values: new object[] { 1, 8 });

            migrationBuilder.InsertData(
                table: "CharacterFilms",
                columns: new[] { "CharacterId", "FilmId" },
                values: new object[] { 1, 9 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CharacterFilms",
                keyColumns: new[] { "CharacterId", "FilmId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "CharacterFilms",
                keyColumns: new[] { "CharacterId", "FilmId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "CharacterFilms",
                keyColumns: new[] { "CharacterId", "FilmId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "CharacterFilms",
                keyColumns: new[] { "CharacterId", "FilmId" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "CharacterFilms",
                keyColumns: new[] { "CharacterId", "FilmId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "CharacterFilms",
                keyColumns: new[] { "CharacterId", "FilmId" },
                keyValues: new object[] { 1, 8 });

            migrationBuilder.DeleteData(
                table: "CharacterFilms",
                keyColumns: new[] { "CharacterId", "FilmId" },
                keyValues: new object[] { 1, 9 });
        }
    }
}
