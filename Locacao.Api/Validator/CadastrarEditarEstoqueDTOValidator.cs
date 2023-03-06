using FluentValidation;
using Locacao.Api.Models.Dto;

namespace Locacao.Api.Validator;

public class CadastrarEditarEstoqueDTOValidator : AbstractValidator<CadastrarEditarEstoqueDTO>
{
    public CadastrarEditarEstoqueDTOValidator()
    {
        RuleFor(a => a.ProdutoId).GreaterThan(0).WithMessage("O produto é inválido");
    }
}