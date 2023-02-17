using FluentValidation;
using Locacao.Api.Models.Dto;
using Locacao.Api.Models.Enums;

namespace Locacao.Api.Validator;

public class ApplicationUserCriarUsuarioDTOValidator : AbstractValidator<ApplicationUserCriarUsuarioDTO>
{
    public ApplicationUserCriarUsuarioDTOValidator()
    {
        RuleFor(a => a.Senha).Must(a => !string.IsNullOrWhiteSpace(a)).WithMessage("Informe a senha");
        RuleFor(a => a.Nome).Must(a => !string.IsNullOrWhiteSpace(a)).WithMessage("Informe o nome do usuário");
        RuleFor(a => a.Sobrenome).Must(a => !string.IsNullOrWhiteSpace(a)).WithMessage("Informe o sobrenome do usuário");
        RuleFor(a => a.Role).Must(a => !string.IsNullOrWhiteSpace(a)).WithMessage("Informe o perfil do usuário");
        RuleFor(a => a.Role).Must(value => Enum.IsDefined(typeof(TipoRoles), value)).WithMessage("O perfil informado é inválido. Adicione um perfil válido");
        RuleFor(a => a.Email).Must(a => !string.IsNullOrWhiteSpace(a)).WithMessage("Informe o e-mail do usuário");
    }
}