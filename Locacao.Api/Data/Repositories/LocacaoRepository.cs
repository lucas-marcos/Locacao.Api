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
                        && a.StatusDaLocacao == StatusDaLocacao.AnaliseeAceita)
            .SelectMany(a => a.ProdutoPorLocacao)
            .Where(a => a.ProdutoId == produtoId)
            .Sum(a => a.Quantidade);
    }

    public IQueryable<Models.Locacao> RetornarLocacoesPeloStatusDaLocacao(StatusDaLocacao statusDaLocacao)
    {
        return RetornarLocacoes()
            .Where(a => a.StatusDaLocacao == statusDaLocacao);
    }

    public IQueryable<Models.Locacao> RetornarLocacoes()
    {
        return BuscarTodos()
            .Include(a => a.EnderecoDoEvento)
            .Include(a => a.UsuarioQueSolicitou)
            .Include(a => a.ProdutoPorLocacao)
            .ThenInclude(a => a.Produto);
    }
}