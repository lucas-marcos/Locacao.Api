namespace Locacao.Api.Framework.CustomAttribute;

public class StatusDaSolicitacaoAttribute : Attribute
{
    public string Nome { get; private set; }

    /// <summary>
    /// Esse status é usado na aba de solicitação no front.
    /// </summary>
    /// <param name="nome"></param>
    public StatusDaSolicitacaoAttribute(string nome)
    {
        Nome = nome;
    }
}