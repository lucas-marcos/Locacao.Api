using Locacao.Api.Controllers.Filters;
using Locacao.Api.Models.Enums;
using Locacao.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Locacao.Api.Controllers;

[ApiController, Route("api/analise-de-dados")]
public class AnaliseDeDadosController
{
    private readonly IAnaliseDeDadosServices _analiseDeDadosServices;

    public AnaliseDeDadosController(IAnaliseDeDadosServices analiseDeDadosServices)
    {
        _analiseDeDadosServices = analiseDeDadosServices;
    }

    /// <summary>
    /// Retorna o resumo por produto das locações CONCLUÍDAS do período informado
    /// </summary>
    /// <param name="dataInicial">Data inicial do período a ser analisado</param>
    /// <param name="dataFinal">Data final do período a ser analisado</param>
    /// <returns>Retorna um objeto com a propriedade "sucesso" indicando se a operação foi concluída com êxito e a propriedade "resumoDaLocacao" contendo as informações do resumo por produto das locações concluídas</returns>
    [HttpGet, Route("resumo-das-locacoes-concluidas/{dataInicial}/{dataFinal}")]
    // [CustomAuthorizationFilter(TipoRoles.Administrador)]
    public object RetornarAnaliseDeLocacao(DateTime dataInicial, DateTime dataFinal)
    {
        try
        {
            var resumoDaLocacao = _analiseDeDadosServices.EstatisticasDoProdutoPorLocacaoPelaData(dataInicial, dataFinal);

            return new { sucesso = true, resumoDaLocacao };
        }
        catch (Exception ex)
        {
            return new { sucesso = false, mensagem = "Não foi possível realizar a operação pelo seguinte motivo: " + ex.Message };
        }
    }
}