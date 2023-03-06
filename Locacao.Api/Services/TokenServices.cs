using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Locacao.Api.Configuration;
using Locacao.Api.Models;
using Locacao.Api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Locacao.Api.Services;

public class TokenServices : ITokenServices
{
    public string GerarToken(ApplicationUser usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Settings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, usuario.UserName),
                new Claim(ClaimTypes.Role, usuario.Role),
                new Claim("UsuarioId", usuario.Id)
            }),
            
            Expires = DateTime.UtcNow.AddHours(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}