using Locacao.Api.Models;

namespace Locacao.Api.Data.Interfaces;

public interface IEstoqueRepository : IRepository<Estoque>
{
    /// <summary>
    /// Irá retornar o Estoque dando innerjoin com o produto
    /// </summary>
    IQueryable<Estoque> RetornarEstoquesEProdutos();
}