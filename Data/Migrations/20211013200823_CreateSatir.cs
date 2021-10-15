using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HaftalıkRaporu.Data.Migrations
{
    public partial class CreateSatir : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Satir",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnemDerecesi = table.Column<int>(type: "int", nullable: false),
                    BaslangisTar = table.Column<DateTime>(type: "Date", nullable: false),
                    BitisTar = table.Column<DateTime>(type: "Date", nullable: false),
                    YapilanIsler = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TalepSahibi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BlanlanmisTar = table.Column<DateTime>(type: "Date", nullable: false),
                    Timeout = table.Column<bool>(type: "bit", nullable: false),
                    HarcananSure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Durumu = table.Column<bool>(type: "bit", nullable: false),
                    Yorumlar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RaporId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Satir", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Satir_Rapors_RaporId",
                        column: x => x.RaporId,
                        principalTable: "Rapors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Satir_RaporId",
                table: "Satir",
                column: "RaporId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Satir");
        }
    }
}
