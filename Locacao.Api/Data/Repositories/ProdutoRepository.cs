using Locacao.Api.Data.Interfaces;
using Locacao.Api.Models;

namespace Locacao.Api.Data.Repositories;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}