using Locacao.Api.Models;

namespace Locacao.Api.Services.Interfaces;

public interface IProdutoServices
{
    void CadatrarProduto(Produto produto);
    void EditarProduto(Produto map);
}