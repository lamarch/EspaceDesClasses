using Microsoft.EntityFrameworkCore.Migrations;

namespace MaClassePA.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstSupprime = table.Column<bool>(type: "bit", nullable: false),
                    Visites = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matieres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstSupprime = table.Column<bool>(type: "bit", nullable: false),
                    Visites = table.Column<long>(type: "bigint", nullable: false),
                    ClasseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matieres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matieres_Classes_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ressources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstSupprime = table.Column<bool>(type: "bit", nullable: false),
                    Contenu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Visites = table.Column<long>(type: "bigint", nullable: false),
                    MatiereId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ressources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ressources_Matieres_MatiereId",
                        column: x => x.MatiereId,
                        principalTable: "Matieres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matieres_ClasseId",
                table: "Matieres",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_Ressources_MatiereId",
                table: "Ressources",
                column: "MatiereId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ressources");

            migrationBuilder.DropTable(
                name: "Matieres");

            migrationBuilder.DropTable(
                name: "Classes");
        }
    }
}
