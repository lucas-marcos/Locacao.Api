using AutoMapper;
using Locacao.Api.Models;
using Locacao.Api.Models.Dto;
using Locacao.Api.Models.TO;
using Locacao.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Locacao.Api.Controllers;

[ApiController, Route("api/produtos")]
public class ProdutoController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IProdutoServices _produtoServices;

    public ProdutoController(IMapper mapper, IProdutoServices produtoServices)
    {
        _mapper = mapper;
        _produtoServices = produtoServices;
    }

    [HttpPost]
    public ActionResult<ProdutoTO> AdicionarProduto(ProdutoDTO produto) //todo verificar quando tem que usar DTO e quando tem que usar TO
    {
        try
        {
            if (!produto.IsValid()) //todo ver dps se tem com validar antes do request chegar no construtor
                throw new Exception(produto.RetornarErros());

            var produtoCadastrado = _produtoServices.CadatrarProduto(_mapper.Map<Produto>(produto));

            return Ok(_mapper.Map<ProdutoTO>(produtoCadastrado));
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Não foi possível adicionar o produto pelo seguinte motivo: " + ex.Message);
        }
    }

    [HttpPut]
    public ActionResult<ProdutoParaEditarDTO> EditarProduto(ProdutoParaEditarDTO produto) //todo verificar quando tem que usar DTO e quando tem que usar TO
    {
        try
        {
            if (!produto.IsValid()) //todo ver dps se tem com validar antes do request chegar no construtor
                throw new Exception(produto.RetornarErros());

            var produtoEditado = _produtoServices.EditarProduto(_mapper.Map<Produto>(produto));

            return Ok(_mapper.Map<ProdutoTO>(produtoEditado));
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Não foi possível editar o produto pelo seguinte motivo: " + ex.Message);
        }
    }

    [HttpDelete, Route("{produtoId}")]
    public ActionResult DeletarProduto(int produtoId)
    {
        try
        {
            _produtoServices.DeletarProduto(produtoId);

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Nãa foi possível deletar o produto pelo seguinte motivo: " + ex.Message);
        }
    }

    [HttpGet]
    public ActionResult<ProdutoTO> ListarProdutos()
    {
        try
        {
            var produtos = _produtoServices.ListarProdutos();

            return Ok(_mapper.Map<List<ProdutoTO>>(produtos));
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Não foi possível listar os produtos pelo seguinte motivo: " + ex.Message );
        }
    }
}