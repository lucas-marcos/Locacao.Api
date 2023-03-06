using Locacao.Api.Models.Dto;
using Locacao.Api.Models.Enums;

namespace Locacao.Api.Services.Interfaces;

public interface ILocacaoServices
{
    void VerificarDisponibilidadeERealizarASolicitacaoDeLocacao(LocacaoCriarSolicitacaoDTO solicitacaoDeLocacao, string usuarioQueSolicitou);
    List<Models.Locacao> RetornarLocacoes();
    List<Models.Locacao> RetornarLocacoesPeloStatusDaLocacao(StatusDaLocacao statusDaLocacao);
}