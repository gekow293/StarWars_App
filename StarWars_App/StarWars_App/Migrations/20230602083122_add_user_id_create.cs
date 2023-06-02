using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarWars_App.Migrations
{
    public partial class add_user_id_create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserIdCreate",
                table: "Characters",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserIdCreate",
                table: "Characters");
        }
    }
}
