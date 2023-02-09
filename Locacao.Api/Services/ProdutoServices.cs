using Locacao.Api.Data.Interfaces;
using Locacao.Api.Models;
using Locacao.Api.Services.Interfaces;

namespace Locacao.Api.Services;

public class ProdutoServices : IProdutoServices
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoServices(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public void CadatrarProduto(Produto produto)
    {
        _produtoRepository.Adicionar(produto);
        _produtoRepository.Salvar();
    }
}