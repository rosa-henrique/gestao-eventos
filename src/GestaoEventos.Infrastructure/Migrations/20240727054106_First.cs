using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoEventos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "eventos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    nome = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    data_hora = table.Column<DateTime>(type: "TEXT", nullable: false),
                    localizacao = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    capacidade_maxima = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventos", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "eventos");
        }
    }
}
