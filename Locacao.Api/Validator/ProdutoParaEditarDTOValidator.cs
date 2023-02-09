using FluentValidation;
using Locacao.Api.Models.Dto;

namespace Locacao.Api.Validator;

public class ProdutoParaEditarDTOValidator: AbstractValidator<ProdutoParaEditarDTO>
{
    public ProdutoParaEditarDTOValidator()
    {
        RuleFor(a => a.Id).GreaterThan(0).WithMessage("Identificador do produto é inválido");
        RuleFor(a => a.Nome).Must(a => !string.IsNullOrWhiteSpace(a)).WithMessage("É necessário informar o nome do produto");
        RuleFor(a => a.Preco).GreaterThan(0).WithMessage("O preço do produto deve sair maior que 0");

    }
}