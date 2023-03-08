namespace Locacao.Api.Models.TO;

public class ProdutoDisponivelTO
{
    public Produto Produto { get; set; }
    public int QuantidadeTotal { get; set; }
    public int QuantidadeDisponivel { get; set; }

    public ProdutoDisponivelTO(){}

    public ProdutoDisponivelTO(Produto produto, int quantidadeTotal, int quantidadeDisponivel)
    {
        Produto = produto;
        QuantidadeTotal = quantidadeTotal;
        QuantidadeDisponivel = quantidadeDisponivel;
    }
}