using System.Security.Claims;
using Locacao.Api.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Locacao.Api.Controllers.Filters;

public class CustomAuthorizationFilter : IAuthorizationFilter
{
    private readonly Claim _claim;

    public CustomAuthorizationFilter(TipoRoles roles)
    {
    }

    public CustomAuthorizationFilter(object roles)
    {
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
    }
}