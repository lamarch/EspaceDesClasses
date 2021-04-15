using Microsoft.EntityFrameworkCore.Migrations;

namespace MaClassePA.Migrations
{
    public partial class RepareClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitesCounter<ClasseModel>_Classes_VisitesTrackerId",
                table: "VisitesCounter<ClasseModel>");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VisitesCounter<ClasseModel>",
                table: "VisitesCounter<ClasseModel>");

            migrationBuilder.RenameTable(
                name: "VisitesCounter<ClasseModel>",
                newName: "VisitesCounters_Classes");

            migrationBuilder.RenameIndex(
                name: "IX_VisitesCounter<ClasseModel>_VisitesTrackerId",
                table: "VisitesCounters_Classes",
                newName: "IX_VisitesCounters_Classes_VisitesTrackerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VisitesCounters_Classes",
                table: "VisitesCounters_Classes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitesCounters_Classes_Classes_VisitesTrackerId",
                table: "VisitesCounters_Classes",
                column: "VisitesTrackerId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitesCounters_Classes_Classes_VisitesTrackerId",
                table: "VisitesCounters_Classes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VisitesCounters_Classes",
                table: "VisitesCounters_Classes");

            migrationBuilder.RenameTable(
                name: "VisitesCounters_Classes",
                newName: "VisitesCounter<ClasseModel>");

            migrationBuilder.RenameIndex(
                name: "IX_VisitesCounters_Classes_VisitesTrackerId",
                table: "VisitesCounter<ClasseModel>",
                newName: "IX_VisitesCounter<ClasseModel>_VisitesTrackerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VisitesCounter<ClasseModel>",
                table: "VisitesCounter<ClasseModel>",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitesCounter<ClasseModel>_Classes_VisitesTrackerId",
                table: "VisitesCounter<ClasseModel>",
                column: "VisitesTrackerId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
