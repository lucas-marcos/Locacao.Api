using System.ComponentModel;

namespace Locacao.Api.Models.Enums;

public enum StatusDaLocacao
{
    [Description("AEntregar")]
    AEntregar = 0,
    [Description("Entregue")]
    Entregue = 1,
    [Description("Recolher")]
    Recolher = 2,
    [Description("Concluido")]
    Concluido = 3,
}
