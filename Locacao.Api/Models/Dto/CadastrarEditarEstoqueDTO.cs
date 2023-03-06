using FluentValidation.Results;
using Locacao.Api.Validator;

namespace Locacao.Api.Models.Dto;

public class CadastrarEditarEstoqueDTO : EstoqueDTOBase
{
    private List<ValidationFailure> Erros { get; set; }

    public bool IsValid()
    {
        var estoqueCadastrarDTOValidator = new CadastrarEditarEstoqueDTOValidator();

        Erros = estoqueCadastrarDTOValidator.Validate(this).Errors;
        return Erros.Count == 0;
    }

    public string RetornarErros() => string.Join(", ", Erros.Select(a => a.ErrorMessage).ToList());
}