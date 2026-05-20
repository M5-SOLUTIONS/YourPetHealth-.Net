using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourPetHealth.Migrations
{
    /// <inheritdoc />
    public partial class AddSequences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_RESPONSAVEIS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    SENHA = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    TELEFONE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_RESPONSAVEIS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_VETERINARIOS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    SENHA = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    TELEFONE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    CRMV = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    ESPECIALIDADE = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_VETERINARIOS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_PETS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    RESPONSAVEL_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NOME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    RACA = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    IDADE = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    PESO = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: true),
                    SEXO = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_PETS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_T_PETS_T_RESPONSAVEIS_RESPONSAVEL_ID",
                        column: x => x.RESPONSAVEL_ID,
                        principalTable: "T_RESPONSAVEIS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_CONSULTAS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PET_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    VETERINARIO_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TIPO = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    DESCRICAO = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: true),
                    DATA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    OBSERVACOES = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: true),
                    STATUS = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CONSULTAS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_T_CONSULTAS_T_PETS_PET_ID",
                        column: x => x.PET_ID,
                        principalTable: "T_PETS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_CONSULTAS_T_VETERINARIOS_VETERINARIO_ID",
                        column: x => x.VETERINARIO_ID,
                        principalTable: "T_VETERINARIOS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "T_HISTORICO_CLINICO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PET_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TIPO = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    DESCRICAO = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: true),
                    DATA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_HISTORICO_CLINICO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_T_HISTORICO_CLINICO_T_PETS_PET_ID",
                        column: x => x.PET_ID,
                        principalTable: "T_PETS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_CONSULTAS_PET_ID",
                table: "T_CONSULTAS",
                column: "PET_ID");

            migrationBuilder.CreateIndex(
                name: "IX_T_CONSULTAS_VETERINARIO_ID",
                table: "T_CONSULTAS",
                column: "VETERINARIO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_T_HISTORICO_CLINICO_PET_ID",
                table: "T_HISTORICO_CLINICO",
                column: "PET_ID");

            migrationBuilder.CreateIndex(
                name: "IX_T_PETS_RESPONSAVEL_ID",
                table: "T_PETS",
                column: "RESPONSAVEL_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_CONSULTAS");

            migrationBuilder.DropTable(
                name: "T_HISTORICO_CLINICO");

            migrationBuilder.DropTable(
                name: "T_VETERINARIOS");

            migrationBuilder.DropTable(
                name: "T_PETS");

            migrationBuilder.DropTable(
                name: "T_RESPONSAVEIS");
        }
    }
}
