using System.Security.Claims;
using Locacao.Api.Framework;
using Locacao.Api.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Locacao.Api.Controllers.Filters;

public class CustomAuthorizationFilter : IAuthorizationFilter
{
    private readonly List<TipoRoles> _roles = new();

    public CustomAuthorizationFilter(params TipoRoles[] roles)
    {
        _roles = roles.ToList();
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity.IsAuthenticated)
            context.Result = new StatusCodeResult(401);

        if (!_roles.Any(a => ValidarRole(context.HttpContext, a.GetDescription())))
            context.Result = new StatusCodeResult(403);
    }

    private static bool ValidarRole(HttpContext context, string role) => context.User.IsInRole(TipoRoles.Administrador.GetDescription()) || context.User.IsInRole(role);
}