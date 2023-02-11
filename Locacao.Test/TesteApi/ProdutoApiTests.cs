using System.Text;
using Locacao.Api.Framework;
using Locacao.Api.Models;
using FluentAssertions;

namespace Locacao.Test.TesteApi;

[TestClass]
public class ProdutoApiTests
{
    private readonly string BASE_URL = "https://localhost:7018/api";

    private HttpClient _client;

    [TestInitialize]
    public void Iniciar()
    {
        _client = new HttpClient();

        if (!PodeRodarOsTestes())
            throw new AssertFailedException("O banco de dados não está apontando para Homologacão. Vá no Appsetting.Development.json e aponte para o banco \"LocacaoDbHomologacao\"");
        LimparTabelas();
    }


    [TestMethod]
    public async Task AdicionarProdutoApiTest()
    {
        var produto = new Produto("Nome teste", "Descrição teste", 1234M, "");

        var response = await _client.PostAsync($"{BASE_URL}/produtos", new StringContent(produto.ToJson(), Encoding.UTF8,
            "application/json"));
        var content = response.Content.ReadAsStringAsync().Result.FromJson<Produto>();

        content.Id.Should().BeGreaterThan(0);
        content.Descricao.Should().Be(produto.Descricao);
        content.Nome.Should().Be(produto.Nome);
        content.Preco.Should().Be(produto.Preco);
        content.DataCriacao.Day.Should().Be(produto.DataCriacao.Day);
        content.DataCriacao.Year.Should().Be(produto.DataCriacao.Year);
    }

    private void LimparTabelas()
    {
        _ = _client.GetAsync($"{BASE_URL}/homologacao/limpar-tabela-produtos-e-estoque").Result;
    }

    private bool PodeRodarOsTestes()
    {
        var response = _client.GetAsync($"{BASE_URL}/homologacao/eh-homologacao").Result;

        var content = response.Content.ReadAsStringAsync().Result;

        Boolean.TryParse(content, out bool result);

        return result;
    }
}