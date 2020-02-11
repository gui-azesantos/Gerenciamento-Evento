using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GerenciamentoEvento.Migrations
{
    public partial class AlterandoEventos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evento_Estilo_EstiloId",
                table: "Evento");

            migrationBuilder.DropTable(
                name: "Estilo");

            migrationBuilder.DropIndex(
                name: "IX_Evento_EstiloId",
                table: "Evento");

            migrationBuilder.DropColumn(
                name: "EstiloId",
                table: "Evento");

            migrationBuilder.AddColumn<string>(
                name: "Estilo",
                table: "Evento",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estilo",
                table: "Evento");

            migrationBuilder.AddColumn<int>(
                name: "EstiloId",
                table: "Evento",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Estilo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estilo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evento_EstiloId",
                table: "Evento",
                column: "EstiloId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evento_Estilo_EstiloId",
                table: "Evento",
                column: "EstiloId",
                principalTable: "Estilo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
