using Locacao.Api.Models.Enums;

namespace Locacao.Api.Models.TO;

public class LocacaoTO
{
    public int Id { get; set; }
    public DateTime DataSolicitacao { get; set; }
    public DateTime DataDoEvento { get; set; }
    public List<Produto> Produtos { get; set; }
    public string UsuarioQueSolicitou { get; set; }
    public Endereco enderecoDoEvento { get; set; }
    public StatusDaLocacao StatusDaLocacao { get; set; }
}