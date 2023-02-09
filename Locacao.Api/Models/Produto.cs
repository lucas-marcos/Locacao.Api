namespace Locacao.Api.Models;

public class Produto : Entity
{
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public decimal Preco { get; private set; }
    public byte[] Imagem { get; private set; }
    public DateTime DataCriacao { get; } = DateTime.Now;

    //EF Constructor
    protected Produto() { }

    public Produto(string nome, string descricao, decimal preco, byte[] imagem)
    {
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        Imagem = imagem;
    }

    public void SetNome(string nome)
    {
        Nome = nome;
    }
    public void SetDescricao(string descricao)
    {
        Descricao = descricao;
    }
    public void SetPreco(decimal preco)
    {
        Preco = preco;
    }
    public void SetImagem(byte[] imagem)
    {
        Imagem = imagem;
    }
}