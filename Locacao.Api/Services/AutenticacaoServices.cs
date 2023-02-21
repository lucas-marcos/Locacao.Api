using Locacao.Api.Models;
using Locacao.Api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Locacao.Api.Services;

public class AutenticacaoServices : IAutenticacaoServices
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenServices _tokenServices;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AutenticacaoServices(UserManager<ApplicationUser> userManager, ITokenServices tokenServices, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _tokenServices = tokenServices;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public ApplicationUser Autenticar(ApplicationUser usuario, string senha)
    {
        
        var usuarioCadastrado = _userManager.FindByEmailAsync(usuario.Email).Result 
                      ?? _userManager.FindByNameAsync(usuario.Email).Result 
                      ?? throw  new Exception("Usuário ou senha inválida");
        
        var resultLogin = _signInManager.PasswordSignInAsync(usuarioCadastrado, senha, isPersistent: false, lockoutOnFailure: false).Result;

        if (!resultLogin.Succeeded) throw new Exception("Usuário ou senha inválida");
        
        usuarioCadastrado.SetToken(_tokenServices.GerarToken(usuarioCadastrado));

        return usuarioCadastrado;
    }

    public ApplicationUser CriarUsuario(ApplicationUser usuario, string senha)
    {
        var user = new ApplicationUser(usuario.Nome, usuario.Sobrenome, usuario.Role)
        {
            UserName = usuario.Email,
            Email = usuario.Email,
        };

        var result = _userManager.CreateAsync(user, senha).Result;

        if (result.Errors.Any(e => e.Code == "DuplicateUserName"))
            throw new Exception("O endereço de email já está sendo usado por outro usuário.");

        if (!result.Succeeded) throw new Exception("Encontramos os seguintes erros: " + string.Join(", ", result.Errors.Select(a => a.Description)));
        
        _ = _userManager.AddToRoleAsync(user, usuario.Role).Result;
        
        return user;
    }
}