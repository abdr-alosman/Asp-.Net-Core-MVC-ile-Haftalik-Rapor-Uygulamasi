using Microsoft.EntityFrameworkCore.Migrations;

namespace HaftalıkRaporu.Data.Migrations
{
    public partial class CreateAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Satir_Rapors_RaporId",
                table: "Satir");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Satir",
                table: "Satir");

            migrationBuilder.RenameTable(
                name: "Satir",
                newName: "Satirs");

            migrationBuilder.RenameIndex(
                name: "IX_Satir_RaporId",
                table: "Satirs",
                newName: "IX_Satirs_RaporId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Satirs",
                table: "Satirs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Satirs_Rapors_RaporId",
                table: "Satirs",
                column: "RaporId",
                principalTable: "Rapors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Satirs_Rapors_RaporId",
                table: "Satirs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Satirs",
                table: "Satirs");

            migrationBuilder.RenameTable(
                name: "Satirs",
                newName: "Satir");

            migrationBuilder.RenameIndex(
                name: "IX_Satirs_RaporId",
                table: "Satir",
                newName: "IX_Satir_RaporId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Satir",
                table: "Satir",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Satir_Rapors_RaporId",
                table: "Satir",
                column: "RaporId",
                principalTable: "Rapors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
