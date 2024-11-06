using FinTech.Api.Contract.Usuario;

namespace FinTech.Api.Domain.Services.Interfaces
{
    public interface IUsuarioService : IService<UsuarioRequestContract, UsuarioResponseContract, long>
    {
        Task<UsuarioLoginResponseContract> Autenticar(UsuarioLoginRequestContract usuarioLoginRequest);
        Task<UsuarioResponseContract> Obter(string email);        
    }
}