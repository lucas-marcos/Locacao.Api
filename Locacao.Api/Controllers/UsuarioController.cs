using AutoMapper;
using Locacao.Api.Controllers.Filters;
using Locacao.Api.Models.Enums;
using Locacao.Api.Models.TO;
using Locacao.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Locacao.Api.Controllers;

[Route("api/usuario")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUsuarioServices _usuarioServices;

    public UsuarioController(IMapper mapper, IUsuarioServices usuarioServices)
    {
        _mapper = mapper;
        _usuarioServices = usuarioServices;
    }
    
    [HttpGet]
    [CustomAuthorizationFilter(TipoRoles.Administrador)]
    public object RetornarUsuarios()
    {
        try
        {
            var usuarios = _usuarioServices.BuscarTodos();
            
            return new { sucesso = true, usuarios = _mapper.Map<List<UsuariosParaListarTO>>(usuarios) };
        }
        catch (Exception ex)
        {
            return new { sucesso = false, mensagem = "Não foi possível retornar os usuários pelo seguinte motivo: " + ex.Message };
        }
    }

    [HttpGet, Route("role/{role}")]
    [CustomAuthorizationFilter(TipoRoles.Administrador)]
    public object RetornarUsuarioPorRole(string role)
    {
        try
        {
            var usuarios = _usuarioServices.BuscarTodosPelaRole(role);
            
            return new { sucesso = true, usuarios = _mapper.Map<List<UsuariosParaListarTO>>(usuarios) };
        }
        catch (Exception ex)
        {
            return new { sucesso = false, mensagem = "Não foi possível retornar os usuários pelo seguinte motivo: " + ex.Message };
        }
    }
}