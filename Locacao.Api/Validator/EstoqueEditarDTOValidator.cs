using FluentValidation;
using Locacao.Api.Models.Dto;

namespace Locacao.Api.Validator;

public class EstoqueEditarDTOValidator : AbstractValidator<EstoqueEditarDTO>
{
    public EstoqueEditarDTOValidator()
    {
        RuleFor(a => a.ProdutoId).GreaterThan(0).WithMessage("O produto é inválido");
        RuleFor(a => a.Id).GreaterThan(0).WithMessage("O identificador do estoque deve ser informado");
    }
}