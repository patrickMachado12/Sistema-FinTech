using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinTech.Api.Migrations
{
    /// <inheritdoc />
    public partial class CriarEntidadeAPagar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_naturezaLancamento_usuario_IdUsuario",
                table: "naturezaLancamento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_usuario",
                table: "usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pessoa",
                table: "pessoa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_naturezaLancamento",
                table: "naturezaLancamento");

            migrationBuilder.RenameTable(
                name: "usuario",
                newName: "Usuario");

            migrationBuilder.RenameTable(
                name: "pessoa",
                newName: "Pessoa");

            migrationBuilder.RenameTable(
                name: "naturezaLancamento",
                newName: "NaturezaLancamento");

            migrationBuilder.RenameIndex(
                name: "IX_naturezaLancamento_IdUsuario",
                table: "NaturezaLancamento",
                newName: "IX_NaturezaLancamento_IdUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pessoa",
                table: "Pessoa",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NaturezaLancamento",
                table: "NaturezaLancamento",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "APagar",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdPessoa = table.Column<long>(type: "bigint", nullable: false),
                    IdUsuario = table.Column<long>(type: "bigint", nullable: false),
                    IdNaturezaLancamento = table.Column<long>(type: "bigint", nullable: false),
                    ValorAPagar = table.Column<double>(type: "double precision", nullable: false),
                    ValorPago = table.Column<double>(type: "double precision", nullable: false),
                    Descricao = table.Column<string>(type: "VARCHAR", nullable: false),
                    DataEmissao = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DataReferencia = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DataExclusao = table.Column<DateTime>(type: "timestamp", nullable: true),
                    Observacao = table.Column<string>(type: "VARCHAR", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APagar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_APagar_NaturezaLancamento_IdNaturezaLancamento",
                        column: x => x.IdNaturezaLancamento,
                        principalTable: "NaturezaLancamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_APagar_Pessoa_IdPessoa",
                        column: x => x.IdPessoa,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_APagar_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_APagar_IdNaturezaLancamento",
                table: "APagar",
                column: "IdNaturezaLancamento");

            migrationBuilder.CreateIndex(
                name: "IX_APagar_IdPessoa",
                table: "APagar",
                column: "IdPessoa");

            migrationBuilder.CreateIndex(
                name: "IX_APagar_IdUsuario",
                table: "APagar",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_NaturezaLancamento_Usuario_IdUsuario",
                table: "NaturezaLancamento",
                column: "IdUsuario",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NaturezaLancamento_Usuario_IdUsuario",
                table: "NaturezaLancamento");

            migrationBuilder.DropTable(
                name: "APagar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pessoa",
                table: "Pessoa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NaturezaLancamento",
                table: "NaturezaLancamento");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "usuario");

            migrationBuilder.RenameTable(
                name: "Pessoa",
                newName: "pessoa");

            migrationBuilder.RenameTable(
                name: "NaturezaLancamento",
                newName: "naturezaLancamento");

            migrationBuilder.RenameIndex(
                name: "IX_NaturezaLancamento_IdUsuario",
                table: "naturezaLancamento",
                newName: "IX_naturezaLancamento_IdUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_usuario",
                table: "usuario",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pessoa",
                table: "pessoa",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_naturezaLancamento",
                table: "naturezaLancamento",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_naturezaLancamento_usuario_IdUsuario",
                table: "naturezaLancamento",
                column: "IdUsuario",
                principalTable: "usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
