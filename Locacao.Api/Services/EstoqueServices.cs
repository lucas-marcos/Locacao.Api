using Locacao.Api.Data.Interfaces;
using Locacao.Api.Models;
using Locacao.Api.Services.Interfaces;

namespace Locacao.Api.Services;

public class EstoqueServices : IEstoqueServices
{
    private readonly IEstoqueRepository _estoqueRepository;
    private readonly IProdutoServices _produtoServices;

    public EstoqueServices(IEstoqueRepository estoqueRepository, IProdutoServices produtoServices)
    {
        _estoqueRepository = estoqueRepository;
        _produtoServices = produtoServices;
    }

    public Estoque CadastrarEstoque(Estoque estoque)
    {
        _ = _produtoServices.RetornarProduto(estoque.ProdutoId) ?? throw new Exception("Não foi possível encontrar o produto informado");

        _estoqueRepository.Adicionar(estoque);
        _estoqueRepository.Salvar();

        return estoque;
    }

    public Estoque EditarEstoque(Estoque estoque)
    {
        var estoqueCadastrado = RetornarEstoque(estoque.Id) ?? throw new Exception("Não foi possível encontrar o estoque informado");
        var produtoCadastrado = _produtoServices.RetornarProduto(estoque.ProdutoId) ?? throw new Exception("Não foi possível encontrar o produto informado");

        estoqueCadastrado.SetProduto(produtoCadastrado);
        estoqueCadastrado.SetQuantidade(estoque.Quantidade);

        _estoqueRepository.Atualizar(estoqueCadastrado);
        _estoqueRepository.Salvar();

        return estoqueCadastrado;
    }

    public void DeletarEstoque(int estoqueId)
    {
        var estoqueCadastrado = RetornarEstoque(estoqueId) ?? throw new Exception("Não foi possível encontrar o estoque para deletar");

        _estoqueRepository.Remover(estoqueCadastrado.Id);
        _estoqueRepository.Salvar();
    }

    public List<Estoque> ListarEstoquesEProdutos()
    {
        return _estoqueRepository.RetornarEstoquesEProdutos().ToList();
    }
    public Estoque RetornarEstoque(int estoqueId)
    {
        return _estoqueRepository.BuscarPorId(estoqueId);
    }
}