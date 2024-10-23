using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinTech.Api.Migrations
{
    /// <inheritdoc />
    public partial class CriarEntidadePessoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "naturezaLancamento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdUsuario = table.Column<long>(type: "bigint", nullable: false),
                    Descricao = table.Column<string>(type: "VARCHAR", nullable: false),
                    Observacao = table.Column<string>(type: "VARCHAR", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DataInativacao = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_naturezaLancamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_naturezaLancamento_usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pessoa",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "VARCHAR", nullable: false),
                    Telefone = table.Column<string>(type: "VARCHAR", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pessoa", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_naturezaLancamento_IdUsuario",
                table: "naturezaLancamento",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "naturezaLancamento");

            migrationBuilder.DropTable(
                name: "pessoa");
        }
    }
}
