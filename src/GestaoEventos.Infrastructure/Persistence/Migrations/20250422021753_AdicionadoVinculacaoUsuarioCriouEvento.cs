using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoEventos.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoVinculacaoUsuarioCriouEvento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "criado_por",
                table: "eventos",
                type: "uuid",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "criado_por",
                table: "eventos");
        }
    }
}
