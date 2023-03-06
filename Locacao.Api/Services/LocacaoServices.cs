using Locacao.Api.Data.Interfaces;
using Locacao.Api.Models;
using Locacao.Api.Models.Dto;
using Locacao.Api.Models.Enums;
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
            var produtoCadastrado = _produtoServices.RetornarProduto(produtoSolicitado.ProdutoId);

            ValidarSePodeLocar(produtoCadastrado, solicitacaoDeLocacao.DataDoEvento, produtoSolicitado.Quantidade);

            locacao.AddProdutoPorLocacao(produtoCadastrado, produtoSolicitado.Quantidade);
        }

        var endereco = solicitacaoDeLocacao.EnderecoDoEvento;

        locacao.SetDataDoEvento(solicitacaoDeLocacao.DataDoEvento);
        locacao.SetEnderecoDoEvento(endereco.Rua, endereco.Bairro, endereco.Cidade, endereco.Uf, endereco.Cep);
        locacao.SetUsuarioQueSolicitouId(usuarioQueSolicitou);
        
        _locacaoRepository.Adicionar(locacao);
        _locacaoRepository.Salvar();
    }

    public List<Models.Locacao> RetornarLocacoes() => _locacaoRepository.RetornarLocacoes().ToList();

    public List<Models.Locacao> RetornarLocacoesPeloStatusDaLocacao(StatusDaLocacao statusDaLocacao) => _locacaoRepository.RetornarLocacoesPeloStatusDaLocacao(statusDaLocacao).ToList();
}