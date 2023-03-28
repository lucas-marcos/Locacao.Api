using Locacao.Api.Models;

namespace Locacao.Api.Services.Interfaces;

public interface IUsuarioServices
{
    ApplicationUser BuscarPorId(string id);
}