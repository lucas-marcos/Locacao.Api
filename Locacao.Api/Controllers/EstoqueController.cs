using AutoMapper;
using Locacao.Api.Controllers.Filters;
using Locacao.Api.Models;
using Locacao.Api.Models.Dto;
using Locacao.Api.Models.Enums;
using Locacao.Api.Models.TO;
using Locacao.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Locacao.Api.Controllers;

[ApiController, Route("api/estoques")]
public class EstoqueController
{
    private readonly IMapper _mapper;
    private readonly IEstoqueServices _estoqueServices;
    public EstoqueController(IMapper mapper, IEstoqueServices estoqueServices)
    {
        _mapper = mapper;
        _estoqueServices = estoqueServices;
    }

    [HttpPost]
    [CustomAuthorizationFilter(TipoRoles.Administrador)]
    public object CadastrarEstoque(CadastrarEditarEstoqueDto cadastrarEditarEstoque)
    {
        try
        {
            if (!cadastrarEditarEstoque.IsValid()) //todo ver dps se tem com validar antes do request chegar no construtor
                throw new Exception(cadastrarEditarEstoque.RetornarErros());

            var estoqueCadastrado = _estoqueServices.CadastrarEstoque(_mapper.Map<Produto>(cadastrarEditarEstoque));

            return new { sucesso = true, estoque = _mapper.Map<ProdutoTO>(estoqueCadastrado) };
        }
        catch (Exception ex)
        {
            return new { sucesso = false, mensagem = "Não foi possível cadastrar o estoque pelo seguinte motivo: " + ex.Message };
        }
    }
    
    [HttpPut]
    [CustomAuthorizationFilter(TipoRoles.Administrador)]
    public object EditarEstoque(CadastrarEditarEstoqueDto editarEstoque)
    {
        try
        {
            if (!editarEstoque.IsValid()) //todo ver dps se tem com validar antes do request chegar no construtor
                throw new Exception(editarEstoque.RetornarErros());
    
            var estoqueEditado = _estoqueServices.EditarEstoque(_mapper.Map<Produto>(editarEstoque));
    
            return new { sucesso = true, estoque = _mapper.Map<ProdutoTO>(estoqueEditado) };
        }
        catch (Exception ex)
        {
            return new { sucesso = false, mensagem = "Não foi possível editar o estoque pelo seguinte motivo: " + ex.Message };
        }
    }
    
    [HttpDelete, Route("{produtoId}")]
    [CustomAuthorizationFilter(TipoRoles.Administrador)]
    public object DeletarEstoque(int produtoId)
    {
        try
        {
            _estoqueServices.DeletarEstoque(produtoId);
    
            return new { sucesso = true };
        }
        catch (Exception ex)
        {
            return new { sucesso = false, mensagem = "Não foi possível deletar o estoque pelo seguinte motivo: " + ex.Message };
        }
    }
    
    [HttpGet]
    [CustomAuthorizationFilter(TipoRoles.Usuario)]
    public object RetornarEstoque()
    {
        try
        {
            var estoques = _estoqueServices.ListarProdutos();
    
            return new { sucesso = true, estoques = _mapper.Map<List<Produto>>(estoques) };
        }
        catch (Exception ex)
        {
            return new { sucesso = false, mensagem = "Não foi possível listar o estoque pelo seguinte motivo: " + ex.Message };
        }
    }
}