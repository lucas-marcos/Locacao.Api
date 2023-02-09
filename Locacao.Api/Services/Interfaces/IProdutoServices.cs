using Locacao.Api.Models;

namespace Locacao.Api.Services.Interfaces;

public interface IProdutoServices
{
    void CadatrarProduto(Produto produto);
    void EditarProduto(Produto map);
    void DeletarProduto(int produtoId);
    List<Produto>  ListarProdutos();
}