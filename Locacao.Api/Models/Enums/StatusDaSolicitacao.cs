using System.ComponentModel;
using Locacao.Api.Framework.CustomAttribute;

namespace Locacao.Api.Models.Enums;

public enum StatusDaSolicitacao
{
    [Description("Andamento")]
    [StatusDaLocacao("Andamento")]
    Andamento = 0,
    
    [Description("Aceito")]
    [StatusDaLocacao("Aceito")]
    Aceito = 1,
    
    [Description("AnaliseRecusada")]
    [StatusDaLocacao("AnaliseRecusada")]
    AnaliseRecusada = 2,
}
