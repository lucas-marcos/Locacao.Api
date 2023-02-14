using AutoMapper;
using Locacao.Api.Models;
using Locacao.Api.Models.Dto;
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
    public object CadastrarEstoque(EstoqueCadastrarDTO estoque)
    {
        try
        {
            if (!estoque.IsValid()) //todo ver dps se tem com validar antes do request chegar no construtor
                throw new Exception(estoque.RetornarErros());

            var estoqueCadastrado = _estoqueServices.CadastrarEstoque(_mapper.Map<Estoque>(estoque));

            return new { sucesso = true, estoque = _mapper.Map<EstoqueTO>(estoqueCadastrado) };
        }
        catch (Exception ex)
        {
            return new { sucesso = false, mensagem = "Não foi possível cadastrar o estoque pelo seguinte motivo: " + ex.Message };
        }
    }

    [HttpPut]
    public object EditarEstoque(EstoqueEditarDTO estoque)
    {
        try
        {
            if (!estoque.IsValid()) //todo ver dps se tem com validar antes do request chegar no construtor
                throw new Exception(estoque.RetornarErros());

            var estoqueEditado = _estoqueServices.EditarEstoque(_mapper.Map<Estoque>(estoque));

            return new { sucesso = true, estoque = _mapper.Map<EstoqueTO>(estoqueEditado) };
        }
        catch (Exception ex)
        {
            return new { sucesso = false, mensagem = "Não foi possível editar o estoque pelo seguinte motivo: " + ex.Message };
        }
    }

    [HttpDelete, Route("{estoqueId}")]
    public object DeletarEstoque(int estoqueId)
    {
        try
        {
            _estoqueServices.DeletarEstoque(estoqueId);

            return new { sucesso = true };
        }
        catch (Exception ex)
        {
            return new { sucesso = false, mensagem = "Não foi possível deletar o estoque pelo seguinte motivo: " + ex.Message };
        }
    }

    [HttpGet]
    public object RetornarEstoque()
    {
        try
        {
            var estoques = _estoqueServices.ListarEstoquesEProdutos();

            return new { sucesso = true, estoques = _mapper.Map<List<EstoqueTO>>(estoques) };
        }
        catch (Exception ex)
        {
            return new { sucesso = false, mensagem = "Não foi possível listar o estoque pelo seguinte motivo: " + ex.Message };
        }
    }
}