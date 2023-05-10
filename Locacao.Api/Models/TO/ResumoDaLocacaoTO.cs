namespace Locacao.Api.Models.TO;

public class ResumoDaLocacaoTO
{
    public ProdutoTO Produto { get; set; }
    public decimal ValorTotalLocado { get; set; }
    public decimal QtdTotalLocado { get; set; }
}