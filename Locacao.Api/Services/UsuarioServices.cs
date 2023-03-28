using Locacao.Api.Data.Interfaces;
using Locacao.Api.Models;
using Locacao.Api.Services.Interfaces;

namespace Locacao.Api.Services;

public class UsuarioServices : IUsuarioServices
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioServices(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public ApplicationUser BuscarPorId(string id) => _usuarioRepository.BuscarPorId(id) ?? throw new Exception("Não foi possível encontrar o usuário");
}