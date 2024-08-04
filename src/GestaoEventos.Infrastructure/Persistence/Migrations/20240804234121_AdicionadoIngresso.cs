using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoEventos.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoIngresso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "eventos_ingressos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    nome = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    descricao = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    preco = table.Column<decimal>(type: "TEXT", precision: 8, scale: 2, nullable: false),
                    quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    EventoId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventos_ingressos", x => x.id);
                    table.ForeignKey(
                        name: "FK_eventos_ingressos_eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "eventos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_eventos_ingressos_EventoId",
                table: "eventos_ingressos",
                column: "EventoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "eventos_ingressos");
        }
    }
}
