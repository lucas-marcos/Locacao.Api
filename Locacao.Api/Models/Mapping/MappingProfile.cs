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

        CreateMap<ApplicationUser, UsuariosParaListarTO>()
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
            .ForMember(dest => dest.Sobrenome, opt => opt.MapFrom(src => src.Sobrenome))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
            .ForMember(dest => dest.DataDeCriacao, opt => opt.MapFrom(src => src.DataDeCriacao))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
        
        CreateMap<Locacao, LocacaoTO>()
            .ForMember(dest => dest.UsuarioQueSolicitou, opt => opt.MapFrom(src => src.UsuarioQueSolicitou.Nome + " " + src.UsuarioQueSolicitou.Sobrenome))
            .ForMember(dest => dest.enderecoDoEvento, opt => opt.MapFrom(src => src.EnderecoDoEvento))
            .ForMember(dest => dest.ProdutoPorLocacao, opt => opt.MapFrom(src => src.ProdutoPorLocacao.ToList()))
            .ForMember(dest => dest.StatusDaLocacao, opt => opt.MapFrom(src => src.StatusDaLocacao.GetDescription()))
            .ForMember(dest => dest.StatusDaSolicitacao, opt => opt.MapFrom(src => src.StatusDaSolicitacao.GetDescription()))
            .ForMember(dest => dest.DataRecolhimentoLocacao, opt => opt.MapFrom(src => src.DataRecolhimentoLocacao));

        CreateMap<LocacaoEditarSolicitacaoDTO, Locacao>()
            .ForMember(dest => dest.StatusDaSolicitacao, opt => opt.MapFrom(src => src.StatusDaSolicitacao.ToEnum<StatusDaSolicitacao>()))
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