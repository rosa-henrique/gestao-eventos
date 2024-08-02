using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoEventos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoStatusEvento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "eventos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "eventos");
        }
    }
}
