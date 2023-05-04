using Locacao.Api.Data.Interfaces;
using Locacao.Api.Models;

namespace Locacao.Api.Data.Repositories;

public class UsuarioRepository : Repository<ApplicationUser>, IUsuarioRepository
{
    public UsuarioRepository(ApplicationDbContext context) : base(context)
    {
    }

    public ApplicationUser? BuscarPorId(string id) => DbSet.FirstOrDefault(a => a.Id == id);
    
    public IQueryable<ApplicationUser> BuscarTodosPelaRole(string role) => DbSet.Where(a => a.Role == role);
}