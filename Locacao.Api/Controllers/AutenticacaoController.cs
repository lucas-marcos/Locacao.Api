using Locacao.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Locacao.Api.Controllers;

[Route("api/login")]
[ApiController]
public class AutenticacaoController : ControllerBase
{
    private readonly IAutenticacaoServices _autenticacaoServices;

    public AutenticacaoController(IAutenticacaoServices autenticacaoServices)
    {
        _autenticacaoServices = autenticacaoServices;
    }

    [HttpPost, Route("login")]
    public void Login(string usuario, string senha)
    {
        _autenticacaoServices.Autenticar(usuario, senha);


    }
}