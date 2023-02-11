using Locacao.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Locacao.Api.Controllers;

[ApiController, Route("api/homologacao")]
public class HomologacaoController
{
    private readonly ApplicationDbContext _applicationDbContext;

    /// <summary>
    /// Esse controller é usado para auxiliar nos teste de unitários
    /// </summary>
    public HomologacaoController(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;

        if (!EstaRodandoComBancoHomologacao())
            throw new Exception("O banco de dados não está apontando para Homologacão. Vá no Appsetting.Development.json e aponte para o banco \"LocacaoDbHomologacao\"");
    }

    [HttpGet, Route("eh-homologacao")]
    public bool EstaRodandoComBancoHomologacao()
    {
        var conexao = _applicationDbContext.Database.GetDbConnection().Database;

        return conexao == "LocacaoDbHomologacao";
    }

    [HttpGet, Route("limpar-tabela-produtos-e-estoque")]
    public void LimparTabelaProdutos()
    {
        var estoque = _applicationDbContext.Estoque;
        var produto = _applicationDbContext.Produto;

        _applicationDbContext.Estoque.RemoveRange(estoque);
        _applicationDbContext.Produto.RemoveRange(produto);

        _applicationDbContext.SaveChanges();
    }
}