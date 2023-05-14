using FluentValidation;
using Locacao.Api.Models.Dto;

namespace Locacao.Api.Validator;

public class LocacaoCriarSolicitacaoDTOValidator : AbstractValidator<LocacaoCriarSolicitacaoDTO>
{
    public LocacaoCriarSolicitacaoDTOValidator()
    {
        RuleFor(a => a.ListaProdutos).Must(a => a.Count > 0).WithMessage("Informe ao menos 1 produto");
        RuleFor(a => a.DataDoEvento).Must(a => a > DateTime.Now).WithMessage("A data do evento deve sair maior do que a data atual");
        RuleFor(a => a.EnderecoDoEvento).NotNull().WithMessage("Informe o endereço do evento");
        RuleFor(a => a)
            .Must(a => a.DataDoEvento <= a.DataRecolhimentoLocacao).WithMessage("A data do recolhimento deve ser maior do que a data do evento");
    }
}