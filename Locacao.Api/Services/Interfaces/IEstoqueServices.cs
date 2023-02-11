using Locacao.Api.Models;

namespace Locacao.Api.Services.Interfaces;

public interface IEstoqueServices
{
    Estoque CadastrarEstoque(Estoque estoque);
    Estoque EditarEstoque(Estoque estoque);
    Estoque RetornarEstoque(int estoqueId);
    /// <summary>
    /// Irá retornar o Estoque dando innerjoin com o produto
    /// </summary>
    List<Estoque> ListarEstoquesEProdutos();
    void DeletarEstoque(int estoqueId);
}