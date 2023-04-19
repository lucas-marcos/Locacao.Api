using Locacao.Api.Models;
using Locacao.Api.Models.Dto;
using Locacao.Api.Models.Enums;
using Locacao.Api.Models.TO;

namespace Locacao.Api.Services.Interfaces;

public interface ILocacaoServices
{
    void VerificarDisponibilidadeERealizarASolicitacaoDeLocacao(LocacaoCriarSolicitacaoDTO solicitacaoDeLocacao, string usuarioQueSolicitou);
    List<Models.Locacao> RetornarLocacoesPeloStatusDaSolicitacao(StatusDaSolicitacao statusDaSolicitacao);
    List<ProdutoDisponivelTO> RetornarProdutosDisponiveisPelaData(DateTime data);
    ProdutoDisponivelTO RetornarProdutoDisponivelPeloProdutoIdEData(int produtoId, DateTime data);
    List<Models.Locacao> RetornarLocacoesPeloUsuarioId(string usuarioId);
    List<Models.Locacao> RetornarLocacoes(ApplicationUser usuario);
    void EditarLocacao(Models.Locacao locacao);
}