using Locacao.Api.Models.Enums;

namespace Locacao.Api.Models;

public class Locacao : Entity
{
    public DateTime DataSolicitacao { get; private set; }
    public DateTime DataDoEvento { get; private set; }
    public DateTime DataRecolhimentoLocacao { get; private set; }
    public ICollection<ProdutoPorLocacao> ProdutoPorLocacao { get; private set; }
    public StatusDaLocacao StatusDaLocacao { get; private set; }
    public StatusDaSolicitacao StatusDaSolicitacao { get; private set; }
    public ApplicationUser UsuarioQueSolicitou { get; private set; }
    public string? UsuarioQueSolicitouId { get; private set; }
    public Endereco EnderecoDoEvento { get; private set; }
    
    //EF Constructor
    public Locacao()
    {
        DataSolicitacao = DateTime.Now;
        StatusDaLocacao = StatusDaLocacao.AEntregar;
        StatusDaSolicitacao = StatusDaSolicitacao.Andamento;
    }

    public Locacao(DateTime dataDoEvento, DateTime dataRecolhimentoLocacao, List<ProdutoPorLocacao> produtoPorLocacao, Endereco enderecoDoEvento, ApplicationUser usuarioQueSolicitou) : this()
    {
        DataDoEvento = dataDoEvento;
        ProdutoPorLocacao = produtoPorLocacao;
        EnderecoDoEvento = enderecoDoEvento;
        UsuarioQueSolicitou = usuarioQueSolicitou;
        DataRecolhimentoLocacao = dataRecolhimentoLocacao;
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
    public void SetStatusDaSolicitacao(StatusDaSolicitacao status) => StatusDaSolicitacao = status;
    public void SetDataRecolhimentoLocacao(DateTime dataRecolhimentoLocacao)
    {
        if (DataDoEvento > dataRecolhimentoLocacao) throw new Exception("A data do recolhimento deve ser maior ou igual a data do evento");

        DataRecolhimentoLocacao = dataRecolhimentoLocacao;
    }
}