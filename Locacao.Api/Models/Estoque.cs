namespace Locacao.Api.Models;

public class Estoque : Entity
{
    public Produto Produto { get; private set; }
    public int Quantidade { get; private set; }
}