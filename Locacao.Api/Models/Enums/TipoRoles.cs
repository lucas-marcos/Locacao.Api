using System.ComponentModel;

namespace Locacao.Api.Models.Enums;

public enum TipoRoles
{
    [Description("Usuario")]
    Usuario = 0,
    
    [Description("Administrador")]
    Administrador = 1,
}