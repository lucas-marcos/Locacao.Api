using AutoMapper;
using Locacao.Api.Models;
using Locacao.Api.Models.Dto;
using Locacao.Api.Models.TO;
using Locacao.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Locacao.Api.Controllers;

[ApiController, Route("api/estoques")]
public class EstoqueController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IEstoqueServices _estoqueServices;

    public EstoqueController(IMapper mapper, IEstoqueServices estoqueServices)
    {
        _mapper = mapper;
        _estoqueServices = estoqueServices;
    }

    [HttpPost]
    public ActionResult<EstoqueTO> CadastrarEstoque(EstoqueCadastrarDTO estoque)
    {
        try
        {
            if (!estoque.IsValid()) //todo ver dps se tem com validar antes do request chegar no construtor
                throw new Exception(estoque.RetornarErros());

            var estoqueCadastrado = _estoqueServices.CadastrarEstoque(_mapper.Map<Estoque>(estoque));

            return Ok(_mapper.Map<EstoqueTO>(estoqueCadastrado));
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Não foi possível cadastrar o estoque pelo seguinte motivo: " + ex.Message);
        }
    }

    [HttpPut]
    public ActionResult<EstoqueTO> EditarEstoque(EstoqueEditarDTO estoque)
    {
        try
        {
            if (!estoque.IsValid()) //todo ver dps se tem com validar antes do request chegar no construtor
                throw new Exception(estoque.RetornarErros());

            var estoqueEditado = _estoqueServices.EditarEstoque(_mapper.Map<Estoque>(estoque));

            return Ok(_mapper.Map<EstoqueTO>(estoqueEditado));
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Não foi possível editar o estoque pelo seguinte motivo: " + ex.Message);
        }
    }

    [HttpDelete, Route("{estoqueId}")]
    public ActionResult<EstoqueTO> DeletarEstoque(int estoqueId)
    {
        try
        {
            _estoqueServices.DeletarEstoque(estoqueId);

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Não foi possível deletar o estoque pelo seguinte motivo: " + ex.Message);
        }
    }

    [HttpGet]
    public ActionResult<List<EstoqueTO>> RetornarEstoque()
    {
        try
        {
            var estoques = _estoqueServices.ListarEstoquesEProdutos();

            return Ok(_mapper.Map<List<EstoqueTO>>(estoques));
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Não foi possível listar o estoque pelo seguinte motivo: " + ex.Message);
        }
    }
}