using System.ComponentModel;

namespace Locacao.Api.Models.Enums;

public enum StatusDaLocacao
{
    [Description("Andamento")]
    Andamento = 0,
    [Description("Aceito")]
    Aceito = 1,
    [Description("AnaliseRecusada")]
    AnaliseRecusada = 2
}
