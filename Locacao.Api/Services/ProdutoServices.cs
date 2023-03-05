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

    public Produto CadatrarProduto(Produto produto)
    {
        _produtoRepository.Adicionar(produto);
        _produtoRepository.Salvar();

        return produto;
    }

    public Produto EditarProduto(Produto produto)
    {
        var produtoCadastrado = _produtoRepository.BuscarPorId(produto.Id) ?? throw new Exception("Não foi possível encontrar produto informado");
        
        //todo verificar se há formas de fazer isso automático. Por reflection não fuincionou por causa do private set
        produtoCadastrado.SetNome(produto.Nome);
        produtoCadastrado.SetDescricao(produto.Descricao);
        produtoCadastrado.SetPreco(produto.Preco);
        produtoCadastrado.SetImagem(produto.Imagem);

        _produtoRepository.Atualizar(produtoCadastrado);
        _produtoRepository.Salvar();

        return produtoCadastrado;
    }

    public void DeletarProduto(int produtoId)
    {
        var produtoCadastrado = _produtoRepository.BuscarPorId(produtoId) ?? throw new Exception("Não foi possível encontrar produto informado");
        
        _produtoRepository.Remover(produtoCadastrado.Id);
        _produtoRepository.Salvar();
    }

    public List<Produto> ListarProdutos()
    {
        return _produtoRepository.BuscarTodos().ToList();
    }

    public Produto RetornarProduto(int produtoId)
    {
        return _produtoRepository.BuscarPorId(produtoId);
    }

    public void Atualizar(Produto produto) => _produtoRepository.Atualizar(produto);
    public void Salvar() => _produtoRepository.Salvar();
}