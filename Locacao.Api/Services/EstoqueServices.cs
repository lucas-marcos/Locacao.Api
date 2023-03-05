using Locacao.Api.Data.Interfaces;
using Locacao.Api.Models;
using Locacao.Api.Services.Interfaces;

namespace Locacao.Api.Services;

public class EstoqueServices : IEstoqueServices
{
    private readonly IProdutoServices _produtoServices;

    public EstoqueServices(IProdutoServices produtoServices)
    {
        _produtoServices = produtoServices;
    }
    
    private Produto SetarQuantidadeEstoque(Produto produto)
    {
        var produtoCadastrado = _produtoServices.RetornarProduto(produto.Id) ?? throw new Exception("Não foi possível encontrar o produto informado");

        produtoCadastrado.SetQuantidade(produto.Quantidade);
        _produtoServices.Atualizar(produtoCadastrado);
        
        _produtoServices.Salvar();

        return produtoCadastrado;
    }

    public Produto CadastrarEstoque(Produto produto) => SetarQuantidadeEstoque(produto);
    public Produto EditarEstoque(Produto produto) => SetarQuantidadeEstoque(produto);
    
    public void DeletarEstoque(int produtoId)
    {
        var produtoCadastrado = _produtoServices.RetornarProduto(produtoId) ?? throw new Exception("Não foi possível encontrar o estoque para deletar");
    
        produtoCadastrado.SetQuantidade(0);
        
        _produtoServices.Atualizar(produtoCadastrado);
        _produtoServices.Salvar();
    }
    
    public List<Produto> ListarProdutos()
    {
        return _produtoServices.ListarProdutos().ToList();
    }
}