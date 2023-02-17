using FluentValidation.Results;
using Locacao.Api.Validator;

namespace Locacao.Api.Models.Dto;

public class ApplicationUserLogarDTO
{
    public string Senha { get; set; }
    public string Email { get; set; }
    
    private List<ValidationFailure> Erros { get; set; }

    public bool IsValid()
    {
        var applicationUserLogarDTOValidator = new ApplicationUserLogarDTOValidator();

        Erros = applicationUserLogarDTOValidator.Validate(this).Errors;
        return Erros.Count == 0;
    }

    public string RetornarErros() => string.Join(", ", Erros.Select(a => a.ErrorMessage).ToList());
}