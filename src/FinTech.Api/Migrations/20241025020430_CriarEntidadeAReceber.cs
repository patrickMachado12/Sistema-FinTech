using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinTech.Api.Migrations
{
    /// <inheritdoc />
    public partial class CriarEntidadeAReceber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AReceber",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdPessoa = table.Column<long>(type: "bigint", nullable: false),
                    IdUsuario = table.Column<long>(type: "bigint", nullable: false),
                    IdNaturezaLancamento = table.Column<long>(type: "bigint", nullable: false),
                    ValorAReceber = table.Column<double>(type: "double precision", nullable: false),
                    ValorBaixado = table.Column<double>(type: "double precision", nullable: false),
                    Descricao = table.Column<string>(type: "VARCHAR", nullable: false),
                    DataEmissao = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DataBaixa = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DataReferencia = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DataExclusao = table.Column<DateTime>(type: "timestamp", nullable: true),
                    Observacao = table.Column<string>(type: "VARCHAR", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AReceber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AReceber_NaturezaLancamento_IdNaturezaLancamento",
                        column: x => x.IdNaturezaLancamento,
                        principalTable: "NaturezaLancamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AReceber_Pessoa_IdPessoa",
                        column: x => x.IdPessoa,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AReceber_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AReceber_IdNaturezaLancamento",
                table: "AReceber",
                column: "IdNaturezaLancamento");

            migrationBuilder.CreateIndex(
                name: "IX_AReceber_IdPessoa",
                table: "AReceber",
                column: "IdPessoa");

            migrationBuilder.CreateIndex(
                name: "IX_AReceber_IdUsuario",
                table: "AReceber",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AReceber");
        }
    }
}
