using AutoMapper;
using Locacao.Api.Controllers.Filters;
using Locacao.Api.Models.Dto;
using Locacao.Api.Models.Enums;
using Locacao.Api.Models.TO;
using Locacao.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Locacao.Api.Controllers;

[ApiController, Route("api/locacao")]
public class LocacaoController : LocacaoControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILocacaoServices _locacaoServices;

    public LocacaoController(IMapper mapper, ILocacaoServices locacaoServices)
    {
        _mapper = mapper;
        _locacaoServices = locacaoServices;
    }

    [HttpPost]
    [CustomAuthorizationFilter(TipoRoles.Usuario)]
    public object CriarSolicitacaoDeLocacao(LocacaoCriarSolicitacaoDTO solicitacao)
    {
        try
        {
            if (!solicitacao.IsValid())
                throw new Exception(solicitacao.RetornarErros());

            _locacaoServices.VerificarDisponibilidadeERealizarASolicitacaoDeLocacao(solicitacao, RetornarUsuarioLogadoId());

            return new { sucesso = true };
        }
        catch (Exception ex)
        {
            return new { sucesso = false, mensagem = "Não foi possível realizar a solicitação pelo seguinte motivo: " + ex.Message };
        }
    }

    [HttpGet]
    [CustomAuthorizationFilter(TipoRoles.Administrador)]
    public object RetornarLocacoes()
    {
        try
        {
            var locacoes = _locacaoServices.RetornarLocacoes();

            return new { sucesso = true, locacoes = _mapper.Map<List<LocacaoTO>>(locacoes) };
        }
        catch (Exception ex)
        {
            return new { sucesso = false, mensagem = "Não foi possível listar as locações pelo seguinte motivo: " + ex.Message };
        }
    }

    [HttpGet, Route("{statusDaLocacao}")]
    [CustomAuthorizationFilter(TipoRoles.Administrador)]
    public object RetornarLocacoesPelosStatus(StatusDaLocacao statusDaLocacao)
    {
        try
        {
            var locacoes = _locacaoServices.RetornarLocacoesPeloStatusDaLocacao(statusDaLocacao);

            return new { sucesso = true, locacoes };
        }
        catch (Exception ex)
        {
            return new { sucesso = false, mensagem = "Não foi possível listar as locações pelo seguinte motivo: " + ex.Message };
        }
    }
}