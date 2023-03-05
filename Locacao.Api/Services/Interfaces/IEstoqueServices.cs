using Locacao.Api.Models;

namespace Locacao.Api.Services.Interfaces;

public interface IEstoqueServices
{
    Produto CadastrarEstoque(Produto estoque);
    Produto EditarEstoque(Produto estoque);
    void DeletarEstoque(int produtoId);
    // /// <summary>
    // /// Irá retornar o Estoque dando innerjoin com o produto
    // /// </summary>
    List<Produto> ListarProdutos();
    // Estoque RetornarEstoquePeloProdutoId(int produtoId);
}