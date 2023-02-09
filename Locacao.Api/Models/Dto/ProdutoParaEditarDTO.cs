using FluentValidation.Results;
using Locacao.Api.Validator;

namespace Locacao.Api.Models.Dto;

public class ProdutoParaEditarDTO : ProdudoDTOBase
{
    public int Id { get; set; }
    
    private List<ValidationFailure> Erros { get; set; }

    public bool IsValid()
    {
        var produtoDTOValidator = new ProdutoParaEditarDTOValidator();

        Erros = produtoDTOValidator.Validate(this).Errors;
        return Erros.Count == 0;
    }

    public string RetornarErros() => string.Join(", ", Erros.Select(a => a.ErrorMessage).ToList());
}