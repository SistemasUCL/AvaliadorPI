using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AvaliadorPI.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Disciplina",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplina", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: false),
                    SobreNome = table.Column<string>(type: "varchar(50)", nullable: false),
                    Telefone = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Administrador",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Administrador_Usuario_Id",
                        column: x => x.Id,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Matricula = table.Column<string>(type: "varchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.Id);
                    table.UniqueConstraint("AK_Aluno_Matricula", x => x.Matricula);
                    table.ForeignKey(
                        name: "FK_Aluno_Usuario_Id",
                        column: x => x.Id,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Avaliador",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avaliador_Usuario_Id",
                        column: x => x.Id,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Professor",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Matricula = table.Column<string>(type: "varchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professor", x => x.Id);
                    table.UniqueConstraint("AK_Professor_Matricula", x => x.Matricula);
                    table.ForeignKey(
                        name: "FK_Professor_Usuario_Id",
                        column: x => x.Id,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssociacaoDisciplinaProfessor",
                columns: table => new
                {
                    DisciplinaId = table.Column<Guid>(nullable: false),
                    ProfessorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociacaoDisciplinaProfessor", x => new { x.DisciplinaId, x.ProfessorId });
                    table.ForeignKey(
                        name: "FK_AssociacaoDisciplinaProfessor_Disciplina_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssociacaoDisciplinaProfessor_Professor_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projeto",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    Periodo = table.Column<string>(nullable: true),
                    ProfessorId = table.Column<Guid>(nullable: false),
                    Tema = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projeto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projeto_Professor_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssociacaoDisciplinaProjeto",
                columns: table => new
                {
                    DisciplinaId = table.Column<Guid>(nullable: false),
                    ProjetoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociacaoDisciplinaProjeto", x => new { x.DisciplinaId, x.ProjetoId });
                    table.ForeignKey(
                        name: "FK_AssociacaoDisciplinaProjeto_Disciplina_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssociacaoDisciplinaProjeto_Projeto_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Criterio",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Descricao = table.Column<string>(type: "varchar(256)", nullable: false),
                    Ordem = table.Column<byte>(nullable: false),
                    Peso = table.Column<byte>(nullable: false),
                    ProjetoId = table.Column<Guid>(nullable: false),
                    Titulo = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criterio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Criterio_Projeto_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grupo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    NomeProjeto = table.Column<string>(nullable: true),
                    ProjetoId = table.Column<Guid>(nullable: false),
                    QRCode = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grupo_Projeto_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssociacaoAlunoGrupo",
                columns: table => new
                {
                    AlunoId = table.Column<Guid>(nullable: false),
                    GrupoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociacaoAlunoGrupo", x => new { x.AlunoId, x.GrupoId });
                    table.ForeignKey(
                        name: "FK_AssociacaoAlunoGrupo_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssociacaoAlunoGrupo_Grupo_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Avaliacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AlunoId = table.Column<Guid>(nullable: true),
                    AvaliadorId = table.Column<Guid>(nullable: false),
                    CriterioId = table.Column<Guid>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    GrupoId = table.Column<Guid>(nullable: true),
                    Nota = table.Column<int>(nullable: false),
                    Tipo = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avaliacao_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Avaliacao_Avaliador_AvaliadorId",
                        column: x => x.AvaliadorId,
                        principalTable: "Avaliador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Avaliacao_Criterio_CriterioId",
                        column: x => x.CriterioId,
                        principalTable: "Criterio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Avaliacao_Grupo_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssociacaoAlunoGrupo_GrupoId",
                table: "AssociacaoAlunoGrupo",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_AssociacaoDisciplinaProfessor_ProfessorId",
                table: "AssociacaoDisciplinaProfessor",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_AssociacaoDisciplinaProjeto_ProjetoId",
                table: "AssociacaoDisciplinaProjeto",
                column: "ProjetoId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_AlunoId",
                table: "Avaliacao",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_AvaliadorId",
                table: "Avaliacao",
                column: "AvaliadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_CriterioId",
                table: "Avaliacao",
                column: "CriterioId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_GrupoId",
                table: "Avaliacao",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_Criterio_ProjetoId",
                table: "Criterio",
                column: "ProjetoId");

            migrationBuilder.CreateIndex(
                name: "IX_Grupo_ProjetoId",
                table: "Grupo",
                column: "ProjetoId");

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_ProfessorId",
                table: "Projeto",
                column: "ProfessorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrador");

            migrationBuilder.DropTable(
                name: "AssociacaoAlunoGrupo");

            migrationBuilder.DropTable(
                name: "AssociacaoDisciplinaProfessor");

            migrationBuilder.DropTable(
                name: "AssociacaoDisciplinaProjeto");

            migrationBuilder.DropTable(
                name: "Avaliacao");

            migrationBuilder.DropTable(
                name: "Disciplina");

            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropTable(
                name: "Avaliador");

            migrationBuilder.DropTable(
                name: "Criterio");

            migrationBuilder.DropTable(
                name: "Grupo");

            migrationBuilder.DropTable(
                name: "Projeto");

            migrationBuilder.DropTable(
                name: "Professor");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
