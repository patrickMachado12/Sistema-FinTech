using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinTech.Api.Migrations
{
    /// <inheritdoc />
    public partial class CriarEntidadeUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "VARCHAR", nullable: false),
                    Senha = table.Column<string>(type: "VARCHAR", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DataInativacao = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NaturezaLancamento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdUsuario = table.Column<long>(type: "bigint", nullable: false),
                    Descricao = table.Column<string>(type: "VARCHAR", nullable: false),
                    Observacao = table.Column<string>(type: "VARCHAR", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaturezaLancamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NaturezaLancamento_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "APagar",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ValorAPagar = table.Column<double>(type: "double precision", nullable: false),
                    ValorPago = table.Column<double>(type: "double precision", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IdUsuario = table.Column<long>(type: "bigint", nullable: false),
                    IdNaturezaLancamento = table.Column<long>(type: "bigint", nullable: false),
                    Descricao = table.Column<string>(type: "VARCHAR", nullable: false),
                    DataEmissao = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "timestamp", nullable: false),
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
                        name: "FK_APagar_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AReceber",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ValorAReceber = table.Column<double>(type: "double precision", nullable: false),
                    ValorBaixado = table.Column<double>(type: "double precision", nullable: false),
                    DataRecebimento = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IdUsuario = table.Column<long>(type: "bigint", nullable: false),
                    IdNaturezaLancamento = table.Column<long>(type: "bigint", nullable: false),
                    Descricao = table.Column<string>(type: "VARCHAR", nullable: false),
                    DataEmissao = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "timestamp", nullable: false),
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
                        name: "FK_AReceber_Usuario_IdUsuario",
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
                name: "IX_APagar_IdUsuario",
                table: "APagar",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_AReceber_IdNaturezaLancamento",
                table: "AReceber",
                column: "IdNaturezaLancamento");

            migrationBuilder.CreateIndex(
                name: "IX_AReceber_IdUsuario",
                table: "AReceber",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_NaturezaLancamento_IdUsuario",
                table: "NaturezaLancamento",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APagar");

            migrationBuilder.DropTable(
                name: "AReceber");

            migrationBuilder.DropTable(
                name: "NaturezaLancamento");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
