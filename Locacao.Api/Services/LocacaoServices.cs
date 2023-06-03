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

    public IQueryable<Models.Locacao> RetornarLocacoes() => _locacaoRepository.RetornarLocacoes();

    private int RetornarQtdDisponivelDoProdutoParaAData(int produtoId, DateTime dataDoEvento)
    {
        var qtdDoProdutoEmEstoque = _produtoServices.RetornarProduto(produtoId).Quantidade;

        var qtdReservado = RetornarQtdReservadoDoProdutoPelaData(produtoId, dataDoEvento);

        return qtdDoProdutoEmEstoque - qtdReservado;
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
        locacao.SetDataRecolhimentoLocacao(solicitacaoDeLocacao.DataRecolhimentoLocacao);
        locacao.SetUsuarioQueSolicitouId(usuarioQueSolicitou);

        _locacaoRepository.Adicionar(locacao);
        _locacaoRepository.Salvar();
    }

    public List<Models.Locacao> RetornarLocacoes(ApplicationUser usuario)
    {
        if (usuario.EhAdministrador())
            return RetornarLocacoes().ToList();

        return RetornarLocacoesPeloUsuarioId(usuario.Id);
    }

    public List<Models.Locacao> RetornarLocacoesPeloUsuarioId(string usuarioId) => _locacaoRepository.RetornarLocacoesPeloUsuarioId(usuarioId).ToList();

    public List<Models.Locacao> RetornarLocacoesPeloStatusDaSolicitacao(StatusDaSolicitacao statusDaSolicitacao) =>
        _locacaoRepository.RetornarLocacoesPeloStatusDaSolicitacao(statusDaSolicitacao).ToList();

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
        locacaoParaAlterar.SetStatusDaSolicitacao(locacao.StatusDaSolicitacao);
        locacaoParaAlterar.SetEnderecoDoEvento(enderecoParaAtualizar.Rua, enderecoParaAtualizar.Bairro, enderecoParaAtualizar.Cidade, enderecoParaAtualizar.Uf, enderecoParaAtualizar.Cep);
        locacaoParaAlterar.SetDataDoEvento(locacao.DataDoEvento);
        locacaoParaAlterar.SetDataRecolhimentoLocacao(locacao.DataRecolhimentoLocacao);

        _locacaoRepository.Atualizar(locacaoParaAlterar);
        _locacaoRepository.Salvar();
    }

    public Models.Locacao EditarStatusDaLocacao(int locacaoId, StatusDaLocacao statusDaLocacao)
    {
        var locacao = _locacaoRepository.BuscarPorId(locacaoId) ?? throw new Exception("Não foi possível encontrar o produto informado");

        locacao.SetStatusDaLocacao(statusDaLocacao);

        _locacaoRepository.Atualizar(locacao);
        _locacaoRepository.Salvar();

        return locacao;
    }

    public List<ProdutoPorLocacao> RetornarTodosOsProdutosConcluidosDentroDoPeriodo(DateTime dataInicial, DateTime dataFinal)
    {
        return RetornarLocacoes()
            .Where(a => a.DataDoEvento >= dataInicial && a.DataDoEvento <= dataFinal && a.StatusDaLocacao == StatusDaLocacao.Concluido)
            .SelectMany(a => a.ProdutoPorLocacao)
            .ToList();
    }

    public List<ValidacaoDeEstoqueTO> VerificarSeTemEstoque(VerificarEstoqueDTO verificarEstoqueDTO)
    {
        var validacaoDeEstoqueTO = new List<ValidacaoDeEstoqueTO>();

        foreach (var produtoParaValidar in verificarEstoqueDTO.Produtos)
        {
            var produto = _produtoServices.RetornarProduto(produtoParaValidar.Id) ?? throw new Exception("Não foi possível encontrar o produto informado");

            var qtdDisponivel = RetornarQtdDisponivelDoProdutoParaAData(produto.Id, verificarEstoqueDTO.DataDoEvento);

            if (produtoParaValidar.Quantidade > qtdDisponivel)
                validacaoDeEstoqueTO.Add(new ValidacaoDeEstoqueTO(produto.Id, false, $"O produto {produto.Nome} possui apenas {qtdDisponivel} disponível para a data informada"));
            else
                validacaoDeEstoqueTO.Add(new ValidacaoDeEstoqueTO(produto.Id, true, "Produto disponível"));
        }

        return validacaoDeEstoqueTO;
    }
}