using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColetaAPI.Migrations
{
    /// <inheritdoc />
    public partial class RenameModelsToEnglish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key constraint first
            migrationBuilder.DropForeignKey(
                name: "FK_Coletas_Localizacoes_LocalizacaoId",
                table: "Coletas");

            // Drop index
            migrationBuilder.DropIndex(
                name: "IX_Coletas_LocalizacaoId",
                table: "Coletas");

            // Rename tables
            migrationBuilder.RenameTable(
                name: "Localizacoes",
                newName: "Locations");

            migrationBuilder.RenameTable(
                name: "Coletas",
                newName: "Collections");

            // Rename columns
            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Locations",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "LocalizacaoId",
                table: "Collections",
                newName: "LocationId");

            // Recreate index with new name
            migrationBuilder.CreateIndex(
                name: "IX_Collections_LocationId",
                table: "Collections",
                column: "LocationId");

            // Recreate foreign key with new names
            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Locations_LocationId",
                table: "Collections",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key constraint first
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Locations_LocationId",
                table: "Collections");

            // Drop index
            migrationBuilder.DropIndex(
                name: "IX_Collections_LocationId",
                table: "Collections");

            // Rename columns back
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Locations",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Collections",
                newName: "LocalizacaoId");

            // Rename tables back
            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Localizacoes");

            migrationBuilder.RenameTable(
                name: "Collections",
                newName: "Coletas");

            // Recreate index with old name
            migrationBuilder.CreateIndex(
                name: "IX_Coletas_LocalizacaoId",
                table: "Coletas",
                column: "LocalizacaoId");

            // Recreate foreign key with old names
            migrationBuilder.AddForeignKey(
                name: "FK_Coletas_Localizacoes_LocalizacaoId",
                table: "Coletas",
                column: "LocalizacaoId",
                principalTable: "Localizacoes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
