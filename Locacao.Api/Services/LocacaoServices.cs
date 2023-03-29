using Locacao.Api.Data.Interfaces;
using Locacao.Api.Models;
using Locacao.Api.Models.Dto;
using Locacao.Api.Models.Enums;
using Locacao.Api.Models.TO;
using Locacao.Api.Services.Interfaces;

namespace Locacao.Api.Services;

public class LocacaoServices : ILocacaoServices
{
    private readonly IProdutoServices _produtoServices;
    private readonly ILocacaoRepository _locacaoRepository;

    public LocacaoServices(IProdutoServices produtoServices, ILocacaoRepository locacaoRepository)
    {
        _produtoServices = produtoServices;
        _locacaoRepository = locacaoRepository;
    }

    private int RetornarQtdReservadoDoProdutoPelaData(int produtoId, DateTime dataDaReserva) => _locacaoRepository.RetornarQtdLocadoDoProdutoPelaData(produtoId, dataDaReserva);

    private int RetornarQtdDisponivelDoProdutoParaAData(int produtoId, DateTime dataDoEvento)
    {
        var qtdDoProdutoEmEstoque = _produtoServices.RetornarProduto(produtoId).Quantidade;

        var qtdReservado = RetornarQtdReservadoDoProdutoPelaData(produtoId, dataDoEvento);

        return qtdDoProdutoEmEstoque - qtdReservado;
    }

    private void ValidarSePodeLocar(Produto produto, DateTime dataDoEvento, int qtdParaLocar)
    {
        var qtdDisponivel = RetornarQtdDisponivelDoProdutoParaAData(produto.Id, dataDoEvento);

        if (qtdParaLocar > qtdDisponivel)
            throw new Exception($"O produto {produto.Nome} possui apenas {qtdDisponivel} disponível para a data informada");
    }

    public void VerificarDisponibilidadeERealizarASolicitacaoDeLocacao(LocacaoCriarSolicitacaoDTO solicitacaoDeLocacao, string usuarioQueSolicitou)
    {
        var locacao = new Models.Locacao();

        foreach (var produtoSolicitado in solicitacaoDeLocacao.ListaProdutos)
        {
            var produtoCadastrado = _produtoServices.RetornarProduto(produtoSolicitado.ProdutoId) ?? throw new Exception("Não foi possível encontar um dos produtos informados");

            locacao.AddProdutoPorLocacao(produtoCadastrado, produtoSolicitado.Quantidade);
        }

        var endereco = solicitacaoDeLocacao.EnderecoDoEvento;

        locacao.SetDataDoEvento(solicitacaoDeLocacao.DataDoEvento);
        locacao.SetEnderecoDoEvento(endereco.Rua, endereco.Bairro, endereco.Cidade, endereco.Uf, endereco.Cep);
        locacao.SetUsuarioQueSolicitouId(usuarioQueSolicitou);

        _locacaoRepository.Adicionar(locacao);
        _locacaoRepository.Salvar();
    }

    public List<Models.Locacao> RetornarLocacoes(ApplicationUser usuario)
    {
        if (usuario.EhAdministrador())
            return _locacaoRepository.RetornarLocacoes().ToList();

        return RetornarLocacoesPeloUsuarioId(usuario.Id);
    }

    public List<Models.Locacao> RetornarLocacoesPeloUsuarioId(string usuarioId) => _locacaoRepository.RetornarLocacoesPeloUsuarioId(usuarioId).ToList();

    public List<Models.Locacao> RetornarLocacoesPeloStatusDaLocacao(StatusDaLocacao statusDaLocacao) => _locacaoRepository.RetornarLocacoesPeloStatusDaLocacao(statusDaLocacao).ToList();

    public List<ProdutoDisponivelTO> RetornarProdutosDisponiveisPelaData(DateTime data)
    {
        var disponibilidadeDosProdutos = new List<ProdutoDisponivelTO>();

        var produtos = _produtoServices.ListarProdutos();

        foreach (var produto in produtos)
        {
            var qtdDisponivel = RetornarQtdDisponivelDoProdutoParaAData(produto.Id, data);

            disponibilidadeDosProdutos.Add(new(produto, produto.Quantidade, qtdDisponivel));
        }

        return disponibilidadeDosProdutos;
    }

    public ProdutoDisponivelTO RetornarProdutoDisponivelPeloProdutoIdEData(int produtoId, DateTime data)
    {
        var produto = _produtoServices.RetornarProduto(produtoId) ?? throw new Exception("Não foi possível encontrar o produto informado");

        var qtdDisponivel = RetornarQtdDisponivelDoProdutoParaAData(produto.Id, data);

        return new(produto, produto.Quantidade, qtdDisponivel);
    }

    public void EditarLocacao(Models.Locacao locacao)
    {
        var locacaoParaAlterar = _locacaoRepository.BuscarPorId(locacao.Id) ?? throw new Exception("Não foi possível encontrar o produto");

        var enderecoParaAtualizar = locacao.EnderecoDoEvento;
        
        locacaoParaAlterar.SetStatusDaLocacao(locacao.StatusDaLocacao);
        locacaoParaAlterar.SetEnderecoDoEvento(enderecoParaAtualizar.Rua, enderecoParaAtualizar.Bairro, enderecoParaAtualizar.Cidade, enderecoParaAtualizar.Uf, enderecoParaAtualizar.Cep);
        locacaoParaAlterar.SetDataDoEvento(locacao.DataDoEvento);
        
        _locacaoRepository.Atualizar(locacaoParaAlterar);
        _locacaoRepository.Salvar();
    }
}