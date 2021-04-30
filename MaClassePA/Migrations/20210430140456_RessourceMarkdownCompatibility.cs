using Microsoft.EntityFrameworkCore.Migrations;

namespace MaClassePA.Migrations
{
    public partial class RessourceMarkdownCompatibility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitesCounter<MatiereModel>");

            migrationBuilder.DropTable(
                name: "VisitesCounter<RessourceModel>");

            migrationBuilder.DropTable(
                name: "VisitesCounters_Classes");

            migrationBuilder.AddColumn<string>(
                name: "Rendu",
                table: "Ressources",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rendu",
                table: "Ressources");

            migrationBuilder.CreateTable(
                name: "VisitesCounter<MatiereModel>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstSupprime = table.Column<bool>(type: "bit", nullable: false),
                    Visites = table.Column<long>(type: "bigint", nullable: false),
                    VisitesTrackerId = table.Column<int>(type: "int", nullable: false)
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
                    EstSupprime = table.Column<bool>(type: "bit", nullable: false),
                    Visites = table.Column<long>(type: "bigint", nullable: false),
                    VisitesTrackerId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "VisitesCounters_Classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstSupprime = table.Column<bool>(type: "bit", nullable: false),
                    Visites = table.Column<long>(type: "bigint", nullable: false),
                    VisitesTrackerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitesCounters_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitesCounters_Classes_Classes_VisitesTrackerId",
                        column: x => x.VisitesTrackerId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_VisitesCounters_Classes_VisitesTrackerId",
                table: "VisitesCounters_Classes",
                column: "VisitesTrackerId",
                unique: true);
        }
    }
}
