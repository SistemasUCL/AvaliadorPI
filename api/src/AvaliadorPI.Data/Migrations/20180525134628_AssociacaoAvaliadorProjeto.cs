using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AvaliadorPI.Data.Migrations
{
    public partial class AssociacaoAvaliadorProjeto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssociacaoAvaliadorProjeto",
                columns: table => new
                {
                    AvaliadorId = table.Column<Guid>(nullable: false),
                    ProjetoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociacaoAvaliadorProjeto", x => new { x.AvaliadorId, x.ProjetoId });
                    table.ForeignKey(
                        name: "FK_AssociacaoAvaliadorProjeto_Avaliador_AvaliadorId",
                        column: x => x.AvaliadorId,
                        principalTable: "Avaliador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssociacaoAvaliadorProjeto_Projeto_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssociacaoAvaliadorProjeto_ProjetoId",
                table: "AssociacaoAvaliadorProjeto",
                column: "ProjetoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssociacaoAvaliadorProjeto");
        }
    }
}
