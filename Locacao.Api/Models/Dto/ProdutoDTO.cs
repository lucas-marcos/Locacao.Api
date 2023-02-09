using FluentValidation;
using FluentValidation.Results;
using Locacao.Api.Validator;


namespace Locacao.Api.Models.Dto;

public class ProdutoDTO
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public byte[]? Imagem { get; set; }


    private List<ValidationFailure> Erros { get; set; }

    public bool IsValid()
    {
        var produtoDTOValidator = new ProdutoDTOValidator();

        Erros = produtoDTOValidator.Validate(this).Errors;
        return Erros.Count == 0;
    }

    public string RetornarErros() => string.Join(", ", Erros.Select(a => a.ErrorMessage).ToList());
}