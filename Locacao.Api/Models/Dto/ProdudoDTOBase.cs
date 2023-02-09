namespace Locacao.Api.Models.Dto;

public class ProdudoDTOBase
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public byte[]? Imagem { get; set; }
}