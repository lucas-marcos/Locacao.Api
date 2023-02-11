namespace Locacao.Api.Models;

public class Estoque : Entity
{
    public Produto Produto { get; private set; }
    public int Quantidade { get; private set; }
    public int ProdutoId { get; private set; }

    public void SetQuantidade(int quantidade) => Quantidade = quantidade;
    public void SetProduto(Produto produto) => Produto = produto;
}