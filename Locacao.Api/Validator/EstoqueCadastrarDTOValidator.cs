using FluentValidation;
using Locacao.Api.Models.Dto;

namespace Locacao.Api.Validator;

public class EstoqueCadastrarDTOValidator : AbstractValidator<CadastrarEditarEstoqueDto>
{
    public EstoqueCadastrarDTOValidator()
    {
        RuleFor(a => a.ProdutoId).GreaterThan(0).WithMessage("O produto é inválido");
    }
}