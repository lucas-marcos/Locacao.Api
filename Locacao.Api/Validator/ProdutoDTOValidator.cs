using FluentValidation;
using Locacao.Api.Models.Dto;

namespace Locacao.Api.Validator;

public class ProdutoDTOValidator : AbstractValidator<ProdutoDTO>
{
    public ProdutoDTOValidator()
    {
        RuleFor(a => a.Nome).Must(a => !string.IsNullOrWhiteSpace(a)).WithMessage("É necessário informar o nome do produto");
        RuleFor(a => a.Preco).GreaterThan(0).WithMessage("O preço do produto deve sair maior que 0");

    }
}