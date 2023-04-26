using FluentValidation;
using Locacao.Api.Models.Dto;
using Locacao.Api.Models.Enums;

namespace Locacao.Api.Validator;

public class ApplicationUserCriarUsuarioDTOValidator : AbstractValidator<ApplicationUserCriarUsuarioDTO>
{
    public ApplicationUserCriarUsuarioDTOValidator()
    {
        RuleFor(a => a.Senha).Must(a => !string.IsNullOrWhiteSpace(a)).WithMessage("Informe a senha");
        RuleFor(a => a.Email).Must(a => !string.IsNullOrWhiteSpace(a)).WithMessage("Informe o e-mail do usuário");
    }
}