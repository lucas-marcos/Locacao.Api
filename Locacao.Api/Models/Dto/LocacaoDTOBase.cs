namespace Locacao.Api.Models.Dto;

public class LocacaoDTOBase
{
    public DateTime DataDoEvento { get; set; }
    public EnderecoDoEventoDTO EnderecoDoEvento { get; set; }
}

public class ListaProdutosParaCadastrarDTO
{
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
}

public class EnderecoDoEventoDTO
{
    public string Rua { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Uf { get; set; }
    public string Cep { get; set; }
}