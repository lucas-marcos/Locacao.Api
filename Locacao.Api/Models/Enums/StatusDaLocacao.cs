using System.ComponentModel;

namespace Locacao.Api.Models.Enums;

public enum StatusDaLocacao
{
    [Description("Andamento")]
    EmAnalise = 0,
    [Description("Aceito")]
    AnaliseeAceita = 1,
    [Description("Cancelado")]
    AnaliseRecusada = 2
}