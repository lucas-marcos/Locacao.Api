namespace Locacao.Api.Models.TO;

public class LocacaoTO
{
    public int Id { get; set; }
    public DateTime DataSolicitacao { get; set; }
    public DateTime DataDoEvento { get; set; }
    public List<ProdutoPorLocacao> ProdutoPorLocacao { get; set; }
    public string UsuarioQueSolicitou { get; set; }
    public Endereco enderecoDoEvento { get; set; }
    public string StatusDaLocacao { get; set; }
    public string StatusDaSolicitacao { get; set; }
}