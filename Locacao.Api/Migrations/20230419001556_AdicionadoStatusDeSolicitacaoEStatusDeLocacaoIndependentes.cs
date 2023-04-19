using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Locacao.Api.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoStatusDeSolicitacaoEStatusDeLocacaoIndependentes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusDaSolicitacao",
                table: "Locacao",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusDaSolicitacao",
                table: "Locacao");
        }
    }
}
