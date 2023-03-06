using FluentValidation.Results;

using Locacao.Api.Validator;

namespace Locacao.Api.Models.Dto;

public class LocacaoCriarSolicitacaoDTO
{
    public List<ListaProdutosParaCadastrarDTO> ListaProdutos { get; set; }
    public DateTime DataDoEvento { get; set; }
    public EnderecoDoEventoDTO EnderecoDoEvento { get; set; }
    
    private List<ValidationFailure> Erros { get; set; }

    public bool IsValid()
    {
        var locacaoCriarSolicitacaoDTO = new LocacaoCriarSolicitacaoDTOValidator();

        Erros = locacaoCriarSolicitacaoDTO.Validate(this).Errors;
        return Erros.Count == 0;
    }

    public string RetornarErros() => string.Join(", ", Erros.Select(a => a.ErrorMessage).ToList());
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