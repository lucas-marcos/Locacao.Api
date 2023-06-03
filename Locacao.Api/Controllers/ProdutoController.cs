using AutoMapper;
using Locacao.Api.Controllers.Filters;
using Locacao.Api.Models;
using Locacao.Api.Models.Dto;
using Locacao.Api.Models.Enums;
using Locacao.Api.Models.TO;
using Locacao.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Locacao.Api.Controllers;

[ApiController, Route("api/produtos")]
public class ProdutoController
{
    private readonly IMapper _mapper;
    private readonly IProdutoServices _produtoServices;

    public ProdutoController(IMapper mapper, IProdutoServices produtoServices)
    {
        _mapper = mapper;
        _produtoServices = produtoServices;
    }

    [HttpPost]
    [CustomAuthorizationFilter(TipoRoles.Administrador)]
    public object AdicionarProduto(ProdutoDTO produto) 
    {
        try
        {
            if (!produto.IsValid()) 
                throw new Exception(produto.RetornarErros());

            var produtoCadastrado = _produtoServices.CadatrarProduto(_mapper.Map<Produto>(produto));

            return new { sucesso = true, produto = _mapper.Map<ProdutoTO>(produtoCadastrado) };
        }
        catch (Exception ex)
        {
            return new { sucesso = false, mensagem = "Não foi possível adicionar o produto pelo seguinte motivo: " + ex.Message };
        }
    }

    [HttpPut]
    [CustomAuthorizationFilter(TipoRoles.Administrador)]
    public object EditarProduto(ProdutoParaEditarDTO produto) 
    {
        try
        {
            if (!produto.IsValid()) 
                throw new Exception(produto.RetornarErros());

            var produtoEditado = _produtoServices.EditarProduto(_mapper.Map<Produto>(produto));

            return new { sucesso = true, produto = _mapper.Map<ProdutoTO>(produtoEditado) };
        }
        catch (Exception ex)
        {
            return new { sucesso = false, mensagem = "Não foi possível editar o produto pelo seguinte motivo: " + ex.Message };
        }
    }

    [HttpDelete, Route("{produtoId}")]
    [CustomAuthorizationFilter(TipoRoles.Administrador)]
    public object DeletarProduto(int produtoId)
    {
        try
        {
            _produtoServices.DeletarProduto(produtoId);

            return new { sucesso = true };
        }
        catch (Exception ex)
        {
            return new { sucesso = false, mensagem = "Nãa foi possível deletar o produto pelo seguinte motivo: " + ex.Message };
        }
    }

    [HttpGet]
    [CustomAuthorizationFilter(TipoRoles.Usuario, TipoRoles.Administrador)]
    public object ListarProdutos()
    {
        try
        {
            var produtos = _produtoServices.ListarProdutos();

            return new { sucesso = true, produtos = _mapper.Map<List<ProdutoTO>>(produtos) };
        }
        catch (Exception ex)
        {
            return new { sucesso = false, mensagem = "Não foi possível listar os produtos pelo seguinte motivo: " + ex.Message };
        }
    }
}