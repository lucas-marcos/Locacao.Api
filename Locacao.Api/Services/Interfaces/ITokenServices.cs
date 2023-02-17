using Locacao.Api.Models;

namespace Locacao.Api.Services.Interfaces;

public interface ITokenServices
{
    string GerarToken(ApplicationUser usuario);
}