using System.Security.Claims;
using Locacao.Api.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Locacao.Api.Controllers.Filters;


public class CustomAuthorizationFilterAttribute : TypeFilterAttribute
{
    public CustomAuthorizationFilterAttribute(params TipoRoles[] role) : base(typeof(CustomAuthorizationFilter))
    {
        Arguments = new object[] { role };
    }
}