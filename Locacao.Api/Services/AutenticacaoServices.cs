using Locacao.Api.Models;
using Locacao.Api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Locacao.Api.Services;

public class AutenticacaoServices : IAutenticacaoServices
{
    private readonly UserManager<Login> _userManager;
    
    public AutenticacaoServices()
    {
        
    }
    
    public void Autenticar(string login, string senha)
    {
        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(senha)) throw new Exception();
        
        var a = _userManager.FindByEmailAsync(login).Result ?? _userManager.FindByNameAsync(login).Result;

    }
}