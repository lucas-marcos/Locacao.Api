namespace Locacao.Api.Models.TO;

public class ApplicationUserTO
{
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public DateTime DataDeCriacao { get; set; }
    public string Role { get; set; }
    public string Token { get; set; }
}