using FluentValidation;
using Locacao.Api.Models.Dto;
using Locacao.Api.Models.Enums;

namespace Locacao.Api.Validator;

public class LocacaoEditarSolicitacaoDTOValidator : AbstractValidator<LocacaoEditarSolicitacaoDTO>
{
    public LocacaoEditarSolicitacaoDTOValidator()
    {
        RuleFor(a => a.EnderecoDoEvento).NotNull().WithMessage("Informe o endereço do evento");
        RuleFor(a => a.Id).GreaterThan(0).WithMessage("Informe o identificador");
        RuleFor(a => a.StatusDaSolicitacao).Must(a => Enum.IsDefined(typeof(StatusDaSolicitacao), a)).WithMessage("Informe um status de solicitação válido");
        RuleFor(a => a)
            .Must(a => a.DataDoEvento <= a.DataRecolhimentoLocacao).WithMessage("A data do recolhimento deve ser maior do que a data do evento");
    }
}