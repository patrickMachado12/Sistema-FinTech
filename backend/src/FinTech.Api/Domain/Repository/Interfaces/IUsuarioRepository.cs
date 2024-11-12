using System.Threading.Tasks;
using FinTech.Api.Domain.Models;

namespace FinTech.Api.Domain.Repository.Interfaces
{
    /// <summary>
    /// Interface de repositório para operações específicas relacionadas à entidade Usuario.
    /// Estende as operações CRUD básicas de <see cref="IRepository{Usuario, long}"/>.
    /// </summary>
    public interface IUsuarioRepository : IRepository<Usuario, long>
    {
        /// <summary>
        /// Obtém um usuário pelo endereço de email.
        /// </summary>
        /// <param name="email">Endereço de email do usuário.</param>
        /// <returns>Retorna um objeto <see cref="Usuario"/> representando o usuário encontrado ou null se o usuário não for encontrado.</returns>
        Task<Usuario?> Obter(string email);

        /// <summary>
        /// Obtém um usuário pelo seu Id.
        /// </summary>
        /// <param name="id">Id do usuário.</param>
        /// <returns>Retorna um objeto <see cref="Usuario"/> representando o usuário encontrado.</returns>
        Task<Usuario> ObterPorId(long id);

        /// <summary>
        /// Salva as alterações feitas no contexto do repositório.
        /// </summary>
        /// <returns>Uma tarefa representando a operação de salvamento das alterações.</returns>
        Task SalvarAlteracoes();
    }
}
