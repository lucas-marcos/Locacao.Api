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
        
        CreateMap<ApplicationUserCriarUsuarioDTO, ApplicationUser>();
        CreateMap<ApplicationUserLogarDTO, ApplicationUser>();
        CreateMap<ApplicationUser, ApplicationUserTO>();
        
        CreateMap<Locacao, LocacaoTO>()
            .ForMember(dest => dest.UsuarioQueSolicitou, opt => opt.MapFrom(src => src.UsuarioQueSolicitou.UserName))
            .ForMember(dest => dest.enderecoDoEvento, opt => opt.MapFrom(src => src.EnderecoDoEvento))
            .ForMember(dest => dest.Produtos, opt => opt.MapFrom(src => src.ProdutoPorLocacao.Select(pl => pl.Produto).ToList()));

    }
}