namespace Locacao.Api.Models;

public class Endereco : Entity
{
    public string Rua { get; private set; }
    public string Bairro { get; private set; }
    public string Cidade { get; private set; }
    public string Uf { get; private set; }
    public string Cep { get; private set; }

    //EF Constructor
    protected Endereco() { }

    public Endereco(string rua, string bairro, string cidade, string uf, string cep)
    {
        Rua = rua;
        Bairro = bairro;
        Cidade = cidade;
        Uf = uf;
        Cep = cep;
    }
}