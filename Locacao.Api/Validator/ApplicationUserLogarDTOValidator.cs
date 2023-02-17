using FluentValidation;
using Locacao.Api.Models.Dto;

namespace Locacao.Api.Validator;

public class ApplicationUserLogarDTOValidator : AbstractValidator<ApplicationUserLogarDTO>
{
    public ApplicationUserLogarDTOValidator()
    {
        RuleFor(a => a.Email).Must(a => a.Contains("@") && a.Contains(".")).WithMessage("E-mail inválido");
        RuleFor(a => a.Senha).MinimumLength(1).WithMessage("Senha inválida");
    }
}