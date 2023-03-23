using Locacao.Api.Models.Dto;
using Locacao.Api.Models.Enums;
using Locacao.Api.Models.TO;

namespace Locacao.Api.Services.Interfaces;

public interface ILocacaoServices
{
    void VerificarDisponibilidadeERealizarASolicitacaoDeLocacao(LocacaoCriarSolicitacaoDTO solicitacaoDeLocacao, string usuarioQueSolicitou);
    List<Models.Locacao> RetornarLocacoes();
    List<Models.Locacao> RetornarLocacoesPeloStatusDaLocacao(StatusDaLocacao statusDaLocacao);
    List<ProdutoDisponivelTO> RetornarProdutosDisponiveisPelaData(DateTime data);
    ProdutoDisponivelTO RetornarProdutoDisponivelPeloProdutoIdEData(int produtoId, DateTime data);
    List<Models.Locacao> RetornarLocacoesPeloUsuarioId(string usuarioId);
}