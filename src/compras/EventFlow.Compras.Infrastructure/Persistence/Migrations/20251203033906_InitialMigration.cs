using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventFlow.Compras.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "compra_ingresos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    usuario_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    data_hora_compra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    valor_total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_compra_ingresos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "compra_ingresos_itens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ingresso_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    preco_unitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    quantidade = table.Column<int>(type: "int", nullable: false),
                    compra_ingressos_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_compra_ingresos_itens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_compra_ingresos_itens_compra_ingresos_compra_ingressos_id",
                        column: x => x.compra_ingressos_id,
                        principalTable: "compra_ingresos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_compra_ingresos_itens_compra_ingressos_id",
                table: "compra_ingresos_itens",
                column: "compra_ingressos_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "compra_ingresos_itens");

            migrationBuilder.DropTable(
                name: "compra_ingresos");
        }
    }
}
