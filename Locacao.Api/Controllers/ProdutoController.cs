using AutoMapper;
using Locacao.Api.Models;
using Locacao.Api.Models.Dto;
using Locacao.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Locacao.Api.Controllers;

[ApiController, Route("api/login")]
public class ProdutoController
{
    private readonly IMapper _mapper;
    private readonly IProdutoServices _produtoServices;

    public ProdutoController(IMapper mapper, IProdutoServices produtoServices)
    {
        _mapper = mapper;
        _produtoServices = produtoServices;
    }

    [HttpPost, Route("adicionar-produto")]
    public object AdicionarProduto(ProdutoDTO produto)
    {
        try
        {
            if (!produto.IsValid()) //todo ver dps se tem com validar antes do request chegar no construtor
                throw new Exception(produto.RetornarErros());

            _produtoServices.CadatrarProduto(_mapper.Map<Produto>(produto));

            return new { sucesso = true };
        }
        catch (Exception ex)
        {
            return new { sucesso = false, mensagem = "Não foi possível adicionar o produto pelo seguinte motivo: " + ex.Message };
        }
    }

    [HttpPut, Route("editar-produto")]
    public object EditarProduto(ProdutoParaEditarDTO produto)
    {
        try
        {
            if (!produto.IsValid()) //todo ver dps se tem com validar antes do request chegar no construtor
                throw new Exception(produto.RetornarErros());

            _produtoServices.EditarProduto(_mapper.Map<Produto>(produto));

            return new { sucesso = true };
        }
        catch (Exception ex)
        {
            return new { sucesso = false, mensagem = "Não foi possível editar o produto pelo seguinte motivo: " + ex.Message };
        }
    }

    [HttpDelete, Route("deletar-produto")]
    public object DeletarProduto(int ProdutoId)
    {
        try
        {
            _produtoServices.DeletarProduto(ProdutoId);

            return new { sucesso = true };
        }
        catch (Exception ex)
        {
            return new { sucesso = false, mensagem = "Nãa foi possível deletar o produto pelo seguinte motivo: " + ex.Message };
        }
    }

    [HttpGet, Route("listar-produtos")]
    public object ListarProdutos()
    {
        try
        {
            var produtos = _produtoServices.ListarProdutos();

            return new { sucesso = true, produtos };
        }
        catch (Exception ex)
        {
            return new { sucesso = false, mensagem = "Não foi possível listar os produtos pelo seguinte motivo: " + ex.Message };
        }
    }
}