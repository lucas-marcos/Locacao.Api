using FluentValidation.Results;
using Locacao.Api.Validator;

namespace Locacao.Api.Models.Dto;

public class LocacaoEditarSolicitacaoDTO : LocacaoDTOBase
{
    public int Id { get; set; }
    public string Status { get; set; }
    
    private List<ValidationFailure> Erros { get; set; }

    public bool IsValid()
    {
        var locacaoEditarSolicitacaoDTOValidator = new LocacaoEditarSolicitacaoDTOValidator();

        Erros = locacaoEditarSolicitacaoDTOValidator.Validate(this).Errors;
        return Erros.Count == 0;
    }

    public string RetornarErros() => string.Join(", ", Erros.Select(a => a.ErrorMessage).ToList());
}