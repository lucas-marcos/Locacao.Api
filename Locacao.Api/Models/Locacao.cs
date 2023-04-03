using Locacao.Api.Models.Enums;

namespace Locacao.Api.Models;

public class Locacao : Entity
{
    public DateTime DataSolicitacao { get; private set; }
    public DateTime DataDoEvento { get; private set; }
    public ICollection<ProdutoPorLocacao> ProdutoPorLocacao { get; private set; }
    public StatusDaLocacao StatusDaLocacao { get; private set; }
    public ApplicationUser UsuarioQueSolicitou { get; private set; }
    public string? UsuarioQueSolicitouId { get; private set; }
    public Endereco EnderecoDoEvento { get; private set; }


    //EF Constructo
    public Locacao()
    {
        DataSolicitacao = DateTime.Now;
        StatusDaLocacao = StatusDaLocacao.Andamento;
    }

    public Locacao(DateTime dataDoEvento, List<ProdutoPorLocacao> produtoPorLocacao, Endereco enderecoDoEvento, ApplicationUser usuarioQueSolicitou) : this()
    {
        DataDoEvento = dataDoEvento;
        ProdutoPorLocacao = produtoPorLocacao;
        EnderecoDoEvento = enderecoDoEvento;
        UsuarioQueSolicitou = usuarioQueSolicitou;
    }

    public void AddProdutoPorLocacao(Produto produto, int qtdDoProduto)
    {
        if (ProdutoPorLocacao is null) ProdutoPorLocacao = new List<ProdutoPorLocacao>();

        ProdutoPorLocacao.Add(new(produto.Id, qtdDoProduto));
    }

    public void SetEnderecoDoEvento(string rua, string birro, string cidade, string uf, string cep)
    {
        EnderecoDoEvento = new Endereco(rua, birro, cidade, uf, cep);
    }

    public void SetDataDoEvento(DateTime dataDoEvento) => DataDoEvento = dataDoEvento;

    public void SetUsuarioQueSolicitouId(string usuarioQueSolicitouId) => UsuarioQueSolicitouId = usuarioQueSolicitouId;
    public void SetStatusDaLocacao(StatusDaLocacao status) => StatusDaLocacao = status;
}