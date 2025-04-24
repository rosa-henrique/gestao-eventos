using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoEventos.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoComprasIngressos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "compra_ingresso",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sessao_id = table.Column<Guid>(type: "uuid", nullable: false),
                    data_compra = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    valor_total = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_compra_ingresso", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "compra_ingresso_ingressos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ingresso_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantidade = table.Column<int>(type: "integer", nullable: false),
                    preco_unitario = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: false),
                    compra_ingresso_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_compra_ingresso_ingressos", x => x.id);
                    table.ForeignKey(
                        name: "FK_compra_ingresso_ingressos_compra_ingresso_compra_ingresso_id",
                        column: x => x.compra_ingresso_id,
                        principalTable: "compra_ingresso",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_compra_ingresso_ingressos_compra_ingresso_id",
                table: "compra_ingresso_ingressos",
                column: "compra_ingresso_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "compra_ingresso_ingressos");

            migrationBuilder.DropTable(
                name: "compra_ingresso");
        }
    }
}
