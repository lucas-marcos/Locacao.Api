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

    public void EditarProduto(Produto produto)
    {
        var produtoCadastrado = _produtoRepository.BuscarPorId(produto.Id) ?? throw new Exception("Não foi possível encontrar produto informado");
        
        //todo verificar se há formas de fazer isso automático. Por reflection não fuincionou por causa do private set
        produtoCadastrado.SetNome(produto.Nome);
        produtoCadastrado.SetDescricao(produto.Descricao);
        produtoCadastrado.SetPreco(produto.Preco);
        produtoCadastrado.SetImagem(produto.Imagem);

        _produtoRepository.Atualizar(produtoCadastrado);
        _produtoRepository.Salvar();
    }

    public void DeletarProduto(int produtoId)
    {
        var produtoCadastrado = _produtoRepository.BuscarPorId(produtoId) ?? throw new Exception("Não foi possível encontrar produto informado");
        
        _produtoRepository.Remover(produtoCadastrado.Id);
        _produtoRepository.Salvar();
    }
}