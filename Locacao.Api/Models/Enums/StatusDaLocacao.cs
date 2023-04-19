using System.ComponentModel;
using Locacao.Api.Framework.CustomAttribute;

namespace Locacao.Api.Models.Enums;

public enum StatusDaLocacao
{
    [Description("AEntregar")]
    [StatusDaSolicitacao("AEntregar")]
    AEntregar = 0,
    
    [Description("Entregue")]
    [StatusDaSolicitacao("Entregue")]
    Entregue = 1,
    
    [Description("Recolher")]
    [StatusDaSolicitacao("Recolher")]
    Recolher = 2,
    
    [Description("Concluido")]
    [StatusDaSolicitacao("Concluido")]
    Concluido = 3
}
