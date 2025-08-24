using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCSMDataManager.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaEntitiesRelarotios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "RelatorioSalas",
                type: "datetime2",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<bool>(
                name: "Barulho",
                table: "RelatorioSalas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Turno",
                table: "RelatorioSalas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Barulho",
                table: "RelatorioSalas");

            migrationBuilder.DropColumn(
                name: "Turno",
                table: "RelatorioSalas");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Data",
                table: "RelatorioSalas",
                type: "date",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 50);
        }
    }
}
