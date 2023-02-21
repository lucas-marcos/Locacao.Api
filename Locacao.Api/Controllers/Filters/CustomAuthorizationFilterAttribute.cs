using System.Security.Claims;
using Locacao.Api.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Locacao.Api.Controllers.Filters;


public class CustomAuthorizationFilterAttribute : TypeFilterAttribute
{
    public CustomAuthorizationFilterAttribute(object e) : base(typeof(CustomAuthorizationFilter))
    {
        if (e.GetType().BaseType != typeof(Enum)) throw new Exception("Permissão informada no ClaimAuthorize não e um Enum");
        var enumerador = e as Enum;
        var name = enumerador?.GetType().Name ?? throw new Exception("Autorização não encontrada.");
        Arguments = new object[] { new Claim(name, enumerador.ToString()) };
    }
}