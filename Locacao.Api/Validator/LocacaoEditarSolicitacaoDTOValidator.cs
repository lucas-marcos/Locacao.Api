using FluentValidation;
using Locacao.Api.Models.Dto;
using Locacao.Api.Models.Enums;

namespace Locacao.Api.Validator;

public class LocacaoEditarSolicitacaoDTOValidator : AbstractValidator<LocacaoEditarSolicitacaoDTO>
{
    public LocacaoEditarSolicitacaoDTOValidator()
    {
        RuleFor(a => a.DataDoEvento).Must(a => a > DateTime.Now).WithMessage("A data do evento deve sair maior do que a data atual");
        RuleFor(a => a.EnderecoDoEvento).NotNull().WithMessage("Informe o endereço do evento");
        RuleFor(a => a.Id).GreaterThan(0).WithMessage("Informe o identificador");
        RuleFor(a => a.Status).Must(a => Enum.IsDefined(typeof(StatusDaLocacao), a)).WithMessage("Informe um status válido");
    }
}