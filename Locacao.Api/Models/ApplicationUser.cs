using System.ComponentModel.DataAnnotations.Schema;
using Locacao.Api.Framework;
using Locacao.Api.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace Locacao.Api.Models;

public class ApplicationUser : IdentityUser
{
     public string Nome { get; private set; }
    public string Sobrenome { get; private set; }
    public DateTime DataDeCriacao { get; private set; }
    public string Role { get; private set; }
    
    [NotMapped]
    public string Token { get; private  set; }

    public ApplicationUser()
    {
        DataDeCriacao = DateTime.Now;
    }

    public ApplicationUser(string nome, string sobrenome, string role)
    {
        Nome = nome;
        Sobrenome = sobrenome;
        Role = role;
        
        DataDeCriacao = DateTime.Now;
    }

    public void SetToken(string token) => Token = token;
    public bool EhAdministrador() => Role.ToEnum<TipoRoles>() == TipoRoles.Administrador;
}