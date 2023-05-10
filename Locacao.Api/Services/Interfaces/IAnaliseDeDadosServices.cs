using Locacao.Api.Models.TO;

namespace Locacao.Api.Services.Interfaces;

public interface IAnaliseDeDadosServices
{
    List<ResumoDaLocacaoTO> EstatisticasDoProdutoPorLocacaoPelaData(DateTime dataInicial, DateTime dataFinal);
}