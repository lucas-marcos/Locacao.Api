using Locacao.Api.Models;

namespace Locacao.Api.Data.Interfaces;

public interface IUsuarioRepository : IRepository<ApplicationUser>
{
    ApplicationUser? BuscarPorId(string id);
}