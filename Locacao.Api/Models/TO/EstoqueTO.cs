namespace Locacao.Api.Models.TO;

public class EstoqueTO
{
    public ProdutoTO Produto { get; private set; }
    public int Quantidade { get; private set; }
    public int Id { get; private set; }
}