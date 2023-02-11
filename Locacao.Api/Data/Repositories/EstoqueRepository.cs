using Locacao.Api.Data.Interfaces;
using Locacao.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Locacao.Api.Data.Repositories;

public class EstoqueRepository : Repository<Estoque>, IEstoqueRepository
{
    public EstoqueRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public IQueryable<Estoque> RetornarEstoquesEProdutos()
    {
        return DbSet
            .Include(a => a.Produto);
    }
}