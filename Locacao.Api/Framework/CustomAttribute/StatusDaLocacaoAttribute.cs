namespace Locacao.Api.Framework.CustomAttribute;

public class StatusDaLocacaoAttribute : Attribute
{
    public string Nome { get; private set; }

    /// <summary>
    /// Esse status é usado na aba de tarefas no front.
    /// </summary>
    /// <param name="nome"></param>
    public StatusDaLocacaoAttribute(string nome)
    {
        Nome = nome;
    }
}