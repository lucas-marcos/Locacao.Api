using AutoMapper;
using Locacao.Api.Controllers.Filters;
using Locacao.Api.Models;
using Locacao.Api.Models.Dto;
using Locacao.Api.Models.Enums;
using Locacao.Api.Models.TO;
using Locacao.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Locacao.Api.Controllers;

[Route("api/login")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAutenticacaoServices _autenticacaoServices;

    public LoginController(IMapper mapper, IAutenticacaoServices autenticacaoServices)
    {
        _mapper = mapper;
        _autenticacaoServices = autenticacaoServices;
    }

    [HttpPost, Route("login")]
    public object Login(ApplicationUserLogarDTO usuario)
    {
        try
        {
            if (!usuario.IsValid()) //todo ver dps se tem com validar antes do request chegar no construtor
                throw new Exception(usuario.RetornarErros());
            
            var usuarioLogado = _autenticacaoServices.Autenticar(_mapper.Map<ApplicationUser>(usuario), usuario.Senha);
            
            Response.Cookies.Delete(".AspNetCore.Identity.Application");
            Response.Headers.Remove("Set-Cookie");
            
            return new { sucesso = true, usuario = _mapper.Map<ApplicationUserTO>(usuarioLogado) };
        }
        catch (Exception ex)
        {
            return new { sucesso = false, mensagem = "Não foi possível logar pelo seguinte motivo: " + ex.Message };
        }
    }

    [HttpPost, Route("criar-usuario")]
    public object CriarUsuario(ApplicationUserCriarUsuarioDTO usuario)
    {
        try
        {
            if (!usuario.IsValid()) //todo ver dps se tem com validar antes do request chegar no construtor
                throw new Exception(usuario.RetornarErros());

            var usuarioCadastrado = _autenticacaoServices.CriarUsuario(_mapper.Map<ApplicationUser>(usuario), usuario.Senha);

            return new { sucesso = true, usuario = _mapper.Map<ApplicationUserTO>(usuarioCadastrado) };
        }
        catch (Exception ex)
        {
            return new { sucesso = false, mensagem = "Não foi possível cadastrar o usuário pelo seguinte motivo: " + ex.Message };
        }
    }
}