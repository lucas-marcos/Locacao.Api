using AutoMapper;
using Locacao.Api.Framework;
using Locacao.Api.Models.Dto;
using Locacao.Api.Models.Enums;
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
            .ForMember(dest => dest.UsuarioQueSolicitou, opt => opt.MapFrom(src => src.UsuarioQueSolicitou.Nome))
            .ForMember(dest => dest.EnderecoDoEvento, opt => opt.MapFrom(src => src.EnderecoDoEvento))
            .ForMember(dest => dest.ProdutoPorLocacao, opt => opt.MapFrom(src => src.ProdutoPorLocacao.ToList()))
            .ForMember(dest => dest.StatusDaLocacao, opt => opt.MapFrom(src => src.StatusDaLocacao.GetHashCode()))
            .ForMember(dest => dest.StatusDaSolicitacao, opt => opt.MapFrom(src => src.StatusDaSolicitacao.GetHashCode()));

        CreateMap<LocacaoEditarSolicitacaoDTO, Locacao>()
            // .ForMember(dest => dest.StatusDaLocacao, opt => opt.MapFrom(src => src.Status.ToEnum<StatusDaLocacao>()))
            .ForMember(dest => dest.StatusDaSolicitacao, opt => opt.MapFrom(src => src.Status.ToEnum<StatusDaSolicitacao>()))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.DataDoEvento, opt => opt.MapFrom(src => src.DataDoEvento))
            .ForPath(dest => dest.EnderecoDoEvento.Bairro, opt => opt.MapFrom(src => src.EnderecoDoEvento.Bairro))
            .ForPath(dest => dest.EnderecoDoEvento.Cep, opt => opt.MapFrom(src => src.EnderecoDoEvento.Cep))
            .ForPath(dest => dest.EnderecoDoEvento.Cidade, opt => opt.MapFrom(src => src.EnderecoDoEvento.Cidade))
            .ForPath(dest => dest.EnderecoDoEvento.Rua, opt => opt.MapFrom(src => src.EnderecoDoEvento.Rua))
            .ForPath(dest => dest.EnderecoDoEvento.Uf, opt => opt.MapFrom(src => src.EnderecoDoEvento.Uf));
        // .ForMember(dest => dest.ProdutoPorLocacao, opt => opt.MapFrom(src => src.ListaProdutos));

    }
}