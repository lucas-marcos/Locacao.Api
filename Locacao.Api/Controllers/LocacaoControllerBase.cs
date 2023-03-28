using System.Security.Claims;
using Locacao.Api.Models;
using Locacao.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Locacao.Api.Controllers;

public class LocacaoControllerBase : ControllerBase
{
    protected IUsuarioServices _usuarioServices => HttpContext.RequestServices.GetService<IUsuarioServices>();

    protected ApplicationUser UsuarioLogado => _usuarioServices.BuscarPorId(RetornarUsuarioLogadoId);
    
    public string RetornarUsuarioLogadoId
    {
        get
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return identity?.FindFirst("UsuarioId")?.Value ?? throw new Exception("Não foi encontrado o usuário");
        }
    }
}