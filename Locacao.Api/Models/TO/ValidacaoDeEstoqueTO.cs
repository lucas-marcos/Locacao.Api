namespace Locacao.Api.Models.TO;

public class ValidacaoDeEstoqueTO
{
    public int ProdutoId { get; set; }
    public bool TemEstoque { get; set; }
    public string Mensagem { get; set; }
    
    public ValidacaoDeEstoqueTO(int produtoId, bool temEstoque, string mensagem)
    {
        ProdutoId = produtoId;
        TemEstoque = temEstoque;
        Mensagem = mensagem;
    }
}