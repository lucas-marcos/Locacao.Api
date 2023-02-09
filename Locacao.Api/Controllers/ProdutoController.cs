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
            if (!produto.IsValid())//todo ver dps se tem com validar antes do request chegar no construtor
                throw new Exception(produto.RetornarErros());

            _produtoServices.CadatrarProduto(_mapper.Map<Produto>(produto));
            
            return new { sucesso = true };
        }
        catch (Exception ex)
        {
            return new { sucesso = false, mensagem = "Não foi possível adicionar o produto pelo seguinte motivo: " + ex.Message };
        }
    }
}