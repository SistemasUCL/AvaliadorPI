using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AvaliadorPI.Data.Migrations
{
    public partial class RemovidoChaveMatricula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Professor_Matricula",
                table: "Professor");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Aluno_Matricula",
                table: "Aluno");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Professor_Matricula",
                table: "Professor",
                column: "Matricula");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Aluno_Matricula",
                table: "Aluno",
                column: "Matricula");
        }
    }
}
