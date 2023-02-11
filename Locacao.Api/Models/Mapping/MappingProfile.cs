using AutoMapper;
using Locacao.Api.Models.Dto;
using Locacao.Api.Models.TO;

namespace Locacao.Api.Models.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProdutoDTO, Produto>();
        CreateMap<Produto, ProdutoTO>();
        CreateMap<ProdutoParaEditarDTO, Produto>();
        
        CreateMap<EstoqueCadastrarDTO, Estoque>();
        CreateMap<EstoqueEditarDTO, Estoque>();
        CreateMap<Estoque, EstoqueTO>();
    }
}