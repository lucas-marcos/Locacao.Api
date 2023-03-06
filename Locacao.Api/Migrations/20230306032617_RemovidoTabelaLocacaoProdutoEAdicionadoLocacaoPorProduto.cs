using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Locacao.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemovidoTabelaLocacaoProdutoEAdicionadoLocacaoPorProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocacaoProduto");

            migrationBuilder.AddColumn<int>(
                name: "ProdutoId",
                table: "Locacao",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProdutoPorLocacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    LocacaoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoPorLocacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdutoPorLocacao_Locacao_LocacaoId",
                        column: x => x.LocacaoId,
                        principalTable: "Locacao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProdutoPorLocacao_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locacao_ProdutoId",
                table: "Locacao",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoPorLocacao_LocacaoId",
                table: "ProdutoPorLocacao",
                column: "LocacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoPorLocacao_ProdutoId",
                table: "ProdutoPorLocacao",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locacao_Produto_ProdutoId",
                table: "Locacao",
                column: "ProdutoId",
                principalTable: "Produto",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locacao_Produto_ProdutoId",
                table: "Locacao");

            migrationBuilder.DropTable(
                name: "ProdutoPorLocacao");

            migrationBuilder.DropIndex(
                name: "IX_Locacao_ProdutoId",
                table: "Locacao");

            migrationBuilder.DropColumn(
                name: "ProdutoId",
                table: "Locacao");

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
                name: "IX_LocacaoProduto_ProdutosId",
                table: "LocacaoProduto",
                column: "ProdutosId");
        }
    }
}
