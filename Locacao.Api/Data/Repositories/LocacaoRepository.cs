using Locacao.Api.Data.Interfaces;
using Locacao.Api.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Locacao.Api.Data.Repositories;

public class LocacaoRepository : Repository<Models.Locacao>, ILocacaoRepository
{
    public LocacaoRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public int RetornarQtdLocadoDoProdutoPelaData(int produtoId, DateTime dataDaReserva)
    {
        return BuscarTodos()
            .Include(a => a.ProdutoPorLocacao)
            .Where(a => a.DataSolicitacao.Day == dataDaReserva.Day 
                        && a.DataSolicitacao.Month == dataDaReserva.Month 
                        && a.DataSolicitacao.Year == dataDaReserva.Year
                        && a.StatusDaSolicitacao == StatusDaSolicitacao.Aceito)
            .SelectMany(a => a.ProdutoPorLocacao)
            .Where(a => a.ProdutoId == produtoId)
            .Sum(a => a.Quantidade);
    }

    public IQueryable<Models.Locacao> RetornarLocacoesPeloStatusDaSolicitacao(StatusDaSolicitacao statusDaSolicitacao)
    {
        return RetornarLocacoes()
            .Where(a => a.StatusDaSolicitacao == statusDaSolicitacao);
    }

    public IQueryable<Models.Locacao> RetornarLocacoes()
    {
        return BuscarTodos()
            .Include(a => a.EnderecoDoEvento)
            .Include(a => a.UsuarioQueSolicitou)
            .Include(a => a.ProdutoPorLocacao)
            .ThenInclude(a => a.Produto);
    }
    
    public IQueryable<Models.Locacao> RetornarLocacoesPeloUsuarioId(string usuarioId)
    {
        return RetornarLocacoes()
            .Where(a => a.UsuarioQueSolicitouId == usuarioId);
    }
}