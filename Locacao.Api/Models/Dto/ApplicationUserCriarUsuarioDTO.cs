using FluentValidation.Results;
using Locacao.Api.Validator;

namespace Locacao.Api.Models.Dto;

public class ApplicationUserCriarUsuarioDTO
{
    public string Senha { get; set; }
    public string Email { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Role { get; set; }
    
    private List<ValidationFailure> Erros { get; set; }

    public bool IsValid()
    {
        var applicationUserCriarUsuarioDTOValidator = new ApplicationUserCriarUsuarioDTOValidator();

        Erros = applicationUserCriarUsuarioDTOValidator.Validate(this).Errors;
        return Erros.Count == 0;
    }

    public string RetornarErros() => string.Join(", ", Erros.Select(a => a.ErrorMessage).ToList());
}