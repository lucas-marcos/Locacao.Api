namespace Locacao.Api.Models.TO;

public class UsuariosParaListarTO
{
    public string? Nome { get; private set; }
    public string? Sobrenome { get; private set; }
    public string Role { get; private set; }
    public DateTime DataDeCriacao { get; private set; }
    public string Email { get; set; }
}