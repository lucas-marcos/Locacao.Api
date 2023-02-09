using Locacao.Api.Models.TO;

namespace Locacao.Api.Services.Interfaces;

public interface IAutenticacaoServices
{
 public void Autenticar(string login, string senha);
}