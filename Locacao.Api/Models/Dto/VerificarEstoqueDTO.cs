namespace Locacao.Api.Models.Dto;

public class VerificarEstoqueDTO
{
    public List<ProdutosParaVerificarEstoqueDTO> Produtos { get; set; }
    public DateTime DataDoEvento { get; set; }
}

public class ProdutosParaVerificarEstoqueDTO
{
    public int Id { get; set; }
    public int Quantidade { get; set; }
}