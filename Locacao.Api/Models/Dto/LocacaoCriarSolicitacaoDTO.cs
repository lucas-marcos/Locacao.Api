using FluentValidation.Results;

using Locacao.Api.Validator;

namespace Locacao.Api.Models.Dto;

public class LocacaoCriarSolicitacaoDTO : LocacaoDTOBase
{
    public List<ListaProdutosParaCadastrarDTO> ListaProdutos { get; set; }

    private List<ValidationFailure> Erros { get; set; }

    public bool IsValid()
    {
        var locacaoCriarSolicitacaoDTO = new LocacaoCriarSolicitacaoDTOValidator();

        Erros = locacaoCriarSolicitacaoDTO.Validate(this).Errors;
        return Erros.Count == 0;
    }

    public string RetornarErros() => string.Join(", ", Erros.Select(a => a.ErrorMessage).ToList());
}