using Microsoft.EntityFrameworkCore.Migrations;

namespace MaClassePA.Migrations
{
    public partial class TEST : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visites",
                table: "Ressources");

            migrationBuilder.DropColumn(
                name: "Visites",
                table: "Matieres");

            migrationBuilder.DropColumn(
                name: "Visites",
                table: "Classes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Visites",
                table: "Ressources",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Visites",
                table: "Matieres",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Visites",
                table: "Classes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
