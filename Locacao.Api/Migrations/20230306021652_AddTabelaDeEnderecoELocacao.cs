using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Locacao.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTabelaDeEnderecoELocacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rua = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cep = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataSolicitacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDoEvento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusDaLocacao = table.Column<int>(type: "int", nullable: false),
                    UsuarioQueSolicitouId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EnderecoDoEventoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locacao_AspNetUsers_UsuarioQueSolicitouId",
                        column: x => x.UsuarioQueSolicitouId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Locacao_Endereco_EnderecoDoEventoId",
                        column: x => x.EnderecoDoEventoId,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocacaoProduto",
                columns: table => new
                {
                    LocacoesId = table.Column<int>(type: "int", nullable: false),
                    ProdutosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocacaoProduto", x => new { x.LocacoesId, x.ProdutosId });
                    table.ForeignKey(
                        name: "FK_LocacaoProduto_Locacao_LocacoesId",
                        column: x => x.LocacoesId,
                        principalTable: "Locacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocacaoProduto_Produto_ProdutosId",
                        column: x => x.ProdutosId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locacao_EnderecoDoEventoId",
                table: "Locacao",
                column: "EnderecoDoEventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Locacao_UsuarioQueSolicitouId",
                table: "Locacao",
                column: "UsuarioQueSolicitouId");

            migrationBuilder.CreateIndex(
                name: "IX_LocacaoProduto_ProdutosId",
                table: "LocacaoProduto",
                column: "ProdutosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocacaoProduto");

            migrationBuilder.DropTable(
                name: "Locacao");

            migrationBuilder.DropTable(
                name: "Endereco");
        }
    }
}
