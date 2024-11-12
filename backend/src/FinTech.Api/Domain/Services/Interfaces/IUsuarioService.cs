using FinTech.Api.Contract.Usuario;
using System.Threading.Tasks;

namespace FinTech.Api.Domain.Services.Interfaces
{
    /// <summary>
    /// Interface de serviço para operações relacionadas à entidade Usuário.
    /// Estende as operações CRUD básicas de <see cref="IService{UsuarioRequestContract, UsuarioResponseContract, long}"/>.
    /// </summary>
    public interface IUsuarioService : IService<UsuarioRequestContract, UsuarioResponseContract, long>
    {
        /// <summary>
        /// Autentica um usuário com base em suas credenciais de login.
        /// </summary>
        /// <param name="usuarioLoginRequest">Objeto <see cref="UsuarioLoginRequestContract"/> contendo as credenciais de login do usuário.</param>
        /// <returns>Retorna um objeto <see cref="UsuarioLoginResponseContract"/> com as informações do usuário autenticado.</returns>
        Task<UsuarioLoginResponseContract> Autenticar(UsuarioLoginRequestContract usuarioLoginRequest);

        /// <summary>
        /// Obtém um usuário pelo endereço de email.
        /// </summary>
        /// <param name="email">Endereço de email do usuário.</param>
        /// <returns>Retorna um objeto <see cref="UsuarioResponseContract"/> representando o usuário associado ao email fornecido.</returns>
        Task<UsuarioResponseContract> Obter(string email);        
    }
}









// using FinTech.Api.Contract.Usuario;

// namespace FinTech.Api.Domain.Services.Interfaces
// {
    
//     public interface IUsuarioService : IService<UsuarioRequestContract, UsuarioResponseContract, long>
//     {
//         Task<UsuarioLoginResponseContract> Autenticar(UsuarioLoginRequestContract usuarioLoginRequest);
//         Task<UsuarioResponseContract> Obter(string email);        
//     }
// }