using Microsoft.EntityFrameworkCore.Migrations;

namespace MaClassePA.Migrations
{
    public partial class VisiteTableClasse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VisitesCounter<ClasseModel>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitesTrackerId = table.Column<int>(type: "int", nullable: false),
                    Visites = table.Column<long>(type: "bigint", nullable: false),
                    EstSupprime = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitesCounter<ClasseModel>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitesCounter<ClasseModel>_Classes_VisitesTrackerId",
                        column: x => x.VisitesTrackerId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisitesCounter<MatiereModel>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitesTrackerId = table.Column<int>(type: "int", nullable: false),
                    Visites = table.Column<long>(type: "bigint", nullable: false),
                    EstSupprime = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitesCounter<MatiereModel>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitesCounter<MatiereModel>_Matieres_VisitesTrackerId",
                        column: x => x.VisitesTrackerId,
                        principalTable: "Matieres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisitesCounter<RessourceModel>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitesTrackerId = table.Column<int>(type: "int", nullable: false),
                    Visites = table.Column<long>(type: "bigint", nullable: false),
                    EstSupprime = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitesCounter<RessourceModel>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitesCounter<RessourceModel>_Ressources_VisitesTrackerId",
                        column: x => x.VisitesTrackerId,
                        principalTable: "Ressources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitesCounter<ClasseModel>_VisitesTrackerId",
                table: "VisitesCounter<ClasseModel>",
                column: "VisitesTrackerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VisitesCounter<MatiereModel>_VisitesTrackerId",
                table: "VisitesCounter<MatiereModel>",
                column: "VisitesTrackerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VisitesCounter<RessourceModel>_VisitesTrackerId",
                table: "VisitesCounter<RessourceModel>",
                column: "VisitesTrackerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitesCounter<ClasseModel>");

            migrationBuilder.DropTable(
                name: "VisitesCounter<MatiereModel>");

            migrationBuilder.DropTable(
                name: "VisitesCounter<RessourceModel>");
        }
    }
}
