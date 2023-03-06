namespace Locacao.Api.Models;

public class ProdutoPorLocacao : Entity
{
    public Produto Produto { get; set; }
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }

    //EF Constructor
    protected ProdutoPorLocacao()
    {
    }

    public ProdutoPorLocacao(int produtoId, int quantidade)
    {
        ProdutoId = produtoId;
        Quantidade = quantidade;
    }
}