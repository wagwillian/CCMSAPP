using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCSMDataManager.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaEntitiesRelarotiosOFFssetdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Data",
                table: "RelatorioSalas",
                type: "datetimeoffset",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 50);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "RelatorioSalas",
                type: "datetime2",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldMaxLength: 50);
        }
    }
}
