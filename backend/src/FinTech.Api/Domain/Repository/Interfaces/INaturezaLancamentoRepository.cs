using System.Collections.Generic;
using System.Threading.Tasks;
using FinTech.Api.Domain.Models;

namespace FinTech.Api.Domain.Repository.Interfaces
{
    /// <summary>
    /// Interface de repositório para operações específicas relacionadas à entidade NaturezaLancamento.
    /// Estende as operações CRUD básicas de <see cref="IRepository{NaturezaLancamento, long}"/>.
    /// </summary>
    public interface INaturezaLancamentoRepository : IRepository<NaturezaLancamento, long>
    {
        /// <summary>
        /// Obtém todas as entidades de NaturezaLancamento associadas a um usuário específico.
        /// </summary>
        /// <param name="idUsuario">Id do usuário para filtrar as naturezas de lançamento.</param>
        /// <returns>Uma lista de objetos <see cref="NaturezaLancamento"/> associados ao usuário.</returns>
        Task<IEnumerable<NaturezaLancamento>> ObterPorIdUsuario(long idUsuario);

        /// <summary>
        /// Salva as alterações feitas no contexto do repositório.
        /// </summary>
        /// <returns>Uma tarefa representando a operação de salvamento das alterações.</returns>
        Task SalvarAlteracoes();
    }
}
