using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Locacao.Api.Controllers;

public class LocacaoControllerBase : ControllerBase
{
    public string RetornarUsuarioLogadoId
    {
        get
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return identity?.FindFirst("UsuarioId")?.Value ?? throw new Exception("Não foi encontrado o usuário");
        }
    }
}