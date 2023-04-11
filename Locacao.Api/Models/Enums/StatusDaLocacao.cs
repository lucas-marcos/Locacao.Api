using System.ComponentModel;
using Locacao.Api.Framework.CustomAttribute;

namespace Locacao.Api.Models.Enums;

public enum StatusDaLocacao
{
    [Description("Andamento")]
    [StatusDaSolicitacao("Andamento")]
    Andamento = 0,
    [Description("Aceito")]
    [StatusDaSolicitacao("Aceito")]
    Aceito = 1,
    [Description("AnaliseRecusada")]
    [StatusDaSolicitacao("AnaliseRecusada")]
    AnaliseRecusada = 2,
    
    [Description("AEntregar")]
    [StatusDaSolicitacao("Aceito")]
    AEntregar = 3,
    [Description("Entregue")]
    [StatusDaSolicitacao("Aceito")]
    Entregue = 4,
    [Description("Recolher")]
    [StatusDaSolicitacao("Aceito")]
    Recolher = 5,
    [Description("Concluido")]
    [StatusDaSolicitacao("Aceito")]
    Concluido = 6,
    
}
