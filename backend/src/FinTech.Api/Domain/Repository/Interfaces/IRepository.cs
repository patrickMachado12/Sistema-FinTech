using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinTech.Api.Domain.Repository.Interfaces
{
    /// <summary>
    /// Interface genérica para operações básicas de repositório do tipo CRUD.
    /// </summary>
    /// <typeparam name="T">O tipo da entidade que será manipulada pelo repositório.</typeparam>
    /// <typeparam name="I">O tipo do Id da entidade.</typeparam>
    public interface IRepository<T, I> where T : class
    {
        /// <summary>
        /// Obtém todas as entidades do repositório.
        /// </summary>
        /// <returns>Uma lista de objetos do tipo <typeparamref name="T"/> representando todas as entidades.</returns>
        Task<IEnumerable<T>> Obter();

        /// <summary>
        /// Obtém uma entidade específica pelo seu Id.
        /// </summary>
        /// <param name="id">Id da entidade a ser obtida.</param>
        /// <returns>O objeto do tipo <typeparamref name="T"/> correspondente ao Id ou null se a entidade não for encontrada.</returns>
        Task<T?> Obter(I id);

        /// <summary>
        /// Adiciona uma nova entidade ao repositório.
        /// </summary>
        /// <param name="entidade">A entidade a ser adicionada.</param>
        /// <returns>O objeto do tipo <typeparamref name="T"/> representando a entidade adicionada.</returns>
        Task<T> Adicionar(T entidade);

        /// <summary>
        /// Atualiza uma entidade existente no repositório.
        /// </summary>
        /// <param name="entidade">A entidade com os dados atualizados.</param>
        /// <returns>O objeto do tipo <typeparamref name="T"/> representando a entidade atualizada.</returns>
        Task<T> Atualizar(T entidade);

        /// <summary>
        /// Remove uma entidade do repositório.
        /// </summary>
        /// <param name="entidade">A entidade a ser removida.</param>
        /// <returns>Uma tarefa representando a operação de remoção.</returns>
        Task Deletar(T entidade);
    }
}
