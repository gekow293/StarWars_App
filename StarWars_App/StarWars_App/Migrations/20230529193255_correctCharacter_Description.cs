using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarWars_App.Migrations
{
    public partial class correctCharacter_Description : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Destination",
                table: "Characters",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Characters",
                newName: "Destination");
        }
    }
}
