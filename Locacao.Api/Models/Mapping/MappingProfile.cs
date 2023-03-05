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
        
        CreateMap<CadastrarEditarEstoqueDto, Produto>()
            .ForMember(a => a.Quantidade, map =>map.MapFrom(src => src.Quantidade))
            .ForMember(a => a.Id, map =>map.MapFrom(src => src.ProdutoId));
  
        CreateMap<ApplicationUserCriarUsuarioDTO, ApplicationUser>();
        CreateMap<ApplicationUserLogarDTO, ApplicationUser>();
        CreateMap<ApplicationUser, ApplicationUserTO>();
        
    }
}