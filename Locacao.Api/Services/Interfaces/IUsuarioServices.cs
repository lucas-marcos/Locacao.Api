using Locacao.Api.Models;

namespace Locacao.Api.Services.Interfaces;

public interface IUsuarioServices
{
    ApplicationUser BuscarPorId(string id);
    List<ApplicationUser> BuscarTodos();
    List<ApplicationUser> BuscarTodosPelaRole(string role);
}