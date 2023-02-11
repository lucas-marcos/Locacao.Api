using Locacao.Api.Models;

namespace Locacao.Api.Services.Interfaces;

public interface IProdutoServices
{
    Produto CadatrarProduto(Produto produto);
    Produto EditarProduto(Produto map);
    void DeletarProduto(int produtoId);
    List<Produto>  ListarProdutos();
}