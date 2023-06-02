using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarWars_App.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    NameOrign = table.Column<string>(type: "TEXT", nullable: false),
                    Birthday = table.Column<string>(type: "TEXT", nullable: true),
                    Planet = table.Column<string>(type: "TEXT", nullable: false),
                    Sex = table.Column<string>(type: "TEXT", nullable: false),
                    Race = table.Column<string>(type: "TEXT", nullable: true),
                    Height = table.Column<float>(type: "REAL", nullable: false),
                    HairColor = table.Column<string>(type: "TEXT", nullable: false),
                    EyeColor = table.Column<string>(type: "TEXT", nullable: false),
                    Destination = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterFilms",
                columns: table => new
                {
                    FilmId = table.Column<int>(type: "INTEGER", nullable: false),
                    CharacterId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterFilms", x => new { x.CharacterId, x.FilmId });
                    table.ForeignKey(
                        name: "FK_CharacterFilms_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterFilms_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Birthday", "Destination", "EyeColor", "HairColor", "Height", "Name", "NameOrign", "Planet", "Race", "Sex" },
                values: new object[] { 1, "896", "Описание", "Зеленый", "Белый", 0.66f, "Йода", "Yoda", "Дагоба", "Раса Йоды", "Мужчина" });

            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Эпизод IV — Новая надежда" });

            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Эпизод V — Империя наносит ответный удар" });

            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Эпизод VI — Возвращение джедая" });

            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Эпизод I — Скрытая угроза" });

            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "Name" },
                values: new object[] { 5, "Эпизод II — Атака клонов" });

            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "Name" },
                values: new object[] { 6, "Эпизод III — Месть ситхов" });

            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "Name" },
                values: new object[] { 7, "Эпизод VII — Пробуждение силы" });

            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "Name" },
                values: new object[] { 8, "Эпизод VIII — Последние джедаи" });

            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "Name" },
                values: new object[] { 9, "Эпизод IX — Скайуокер. Восход" });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterFilms_FilmId",
                table: "CharacterFilms",
                column: "FilmId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterFilms");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Films");
        }
    }
}
