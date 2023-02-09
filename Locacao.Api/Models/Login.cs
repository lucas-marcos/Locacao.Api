using System.ComponentModel.DataAnnotations;

namespace Locacao.Api.Models;

public class Login
{
    [Key]
    public int Id { get; set; }
    public string Usuario { get; set; }
    public string Senha { get; set; }
}