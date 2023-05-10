using AutoMapper;
using Locacao.Api.Models.TO;
using Locacao.Api.Services.Interfaces;

namespace Locacao.Api.Services;

public class AnaliseDeDadosServices : IAnaliseDeDadosServices
{
    private readonly IMapper _mapper;
    private readonly IProdutoServices _produtoServices;
    private readonly ILocacaoServices _locacaoServices;

    public AnaliseDeDadosServices(IMapper mapper, IProdutoServices produtoServices, ILocacaoServices locacaoServices)
    {
        _mapper = mapper;
        _produtoServices = produtoServices;
        _locacaoServices = locacaoServices;
    }

    public List<ResumoDaLocacaoTO> EstatisticasDoProdutoPorLocacaoPelaData(DateTime dataInicial, DateTime dataFinal)
    {
        var resumoDasLocacoes = new List<ResumoDaLocacaoTO>();

        var produtosCadastrados = _produtoServices.ListarProdutos();
        var produtoPorLocacaoDoPeriodo = _locacaoServices.RetornarTodosOsProdutosConcluidosDentroDoPeriodo(dataInicial, dataFinal);


        foreach (var produto in produtosCadastrados)
        {
            var produtosDoPeriodo = produtoPorLocacaoDoPeriodo.Where(a => a.ProdutoId == produto.Id).ToList();

            if (produtosDoPeriodo.Count > 0)
                resumoDasLocacoes.Add(new ResumoDaLocacaoTO
                {
                    Produto = _mapper.Map<ProdutoTO>(produto),
                    QtdTotalLocado = produtosDoPeriodo.Sum(a => a.Quantidade),
                    ValorTotalLocado = produtosDoPeriodo.Sum(a => a.Quantidade * produto.Preco)
                });
        }

        return resumoDasLocacoes;
    }
}