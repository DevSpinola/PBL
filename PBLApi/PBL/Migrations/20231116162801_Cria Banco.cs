using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PBL.Migrations
{
    public partial class CriaBanco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meteoro",
                columns: table => new
                {
                    MeteoroId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlturaInicial = table.Column<int>(type: "int", nullable: false),
                    VelocidadeMeteoro = table.Column<int>(type: "int", nullable: false),
                    DistanciaHorizontal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meteoro", x => x.MeteoroId);
                });

            migrationBuilder.CreateTable(
                name: "Projetil",
                columns: table => new
                {
                    ProjetilId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnguloGraus = table.Column<double>(type: "float", nullable: false),
                    AnguloRad = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projetil", x => x.ProjetilId);
                });

            migrationBuilder.CreateTable(
                name: "Colisao",
                columns: table => new
                {
                    ColisaoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoParaColidir = table.Column<double>(type: "float", nullable: true),
                    AlturaColisao = table.Column<double>(type: "float", nullable: true),
                    TempoColisao = table.Column<double>(type: "float", nullable: true),
                    ProjetilId = table.Column<int>(type: "int", nullable: false),
                    MeteoroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colisao", x => x.ColisaoId);
                    table.ForeignKey(
                        name: "FK_Colisao_Meteoro_MeteoroId",
                        column: x => x.MeteoroId,
                        principalTable: "Meteoro",
                        principalColumn: "MeteoroId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Colisao_Projetil_ProjetilId",
                        column: x => x.ProjetilId,
                        principalTable: "Projetil",
                        principalColumn: "ProjetilId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Colisao_MeteoroId",
                table: "Colisao",
                column: "MeteoroId");

            migrationBuilder.CreateIndex(
                name: "IX_Colisao_ProjetilId",
                table: "Colisao",
                column: "ProjetilId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Colisao");

            migrationBuilder.DropTable(
                name: "Meteoro");

            migrationBuilder.DropTable(
                name: "Projetil");
        }
    }
}
