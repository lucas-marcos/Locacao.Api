using Locacao.Api.Models;

namespace Locacao.Api.Services.Interfaces;

public interface IAutenticacaoServices
{
 ApplicationUser Autenticar(ApplicationUser usuario, string senha);
 ApplicationUser CriarUsuario(ApplicationUser usuario, string senha);
}