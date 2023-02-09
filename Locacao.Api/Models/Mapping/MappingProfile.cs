using AutoMapper;
using Locacao.Api.Models.Dto;

namespace Locacao.Api.Models.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProdutoDTO, Produto>();
        CreateMap<ProdutoParaEditarDTO, Produto>();
    }
}