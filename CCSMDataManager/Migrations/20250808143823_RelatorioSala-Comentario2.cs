using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCSMDataManager.Migrations
{
    /// <inheritdoc />
    public partial class RelatorioSalaComentario2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comentario",
                table: "RelatorioSalas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comentario",
                table: "RelatorioSalas");
        }
    }
}
