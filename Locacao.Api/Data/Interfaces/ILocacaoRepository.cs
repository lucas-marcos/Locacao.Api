using Locacao.Api.Models.Enums;

namespace Locacao.Api.Data.Interfaces;

public interface ILocacaoRepository : IRepository<Models.Locacao>
{
    /// <summary>
    /// Irá validar quantos produtos COM STAUTS AnaliseeAceita, pelo id, tem reservado para a data informada
    /// </summary>
    int RetornarQtdLocadoDoProdutoPelaData(int produtoId, DateTime dataDaReserva);

    IQueryable<Models.Locacao> RetornarLocacoesPeloStatusDaSolicitacao(StatusDaSolicitacao statusDaSolicitacao);
    IQueryable<Models.Locacao> RetornarLocacoes();
    IQueryable<Models.Locacao> RetornarLocacoesPeloUsuarioId(string usuarioId);

}