using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinTech.Api.Domain.Services.Interfaces
{
    /// <summary>
    /// Interface genérica para criação de serviços do tipo CRUD relacionados a títulos.
    /// </summary>
    /// <typeparam name="RQ">Tipo do contrato de request, representando os dados de entrada.</typeparam>
    /// <typeparam name="RS">Tipo do contrato de response, representando os dados de saída.</typeparam>
    /// <typeparam name="I">Tipo do Id, usado para identificar entidades e usuários.</typeparam>
    public interface ITituloService<RQ, RS, I> where RQ : class
    {
        /// <summary>
        /// Obtém todos os títulos associados a um usuário.
        /// </summary>
        /// <param name="idUsuario">Id do usuário.</param>
        /// <returns>Uma lista de objetos do tipo <typeparamref name="RS"/> representando os títulos.</returns>
        Task<IEnumerable<RS>> ObterTodos(I idUsuario);

        /// <summary>
        /// Obtém um título específico pelo seu Id e Id do usuário.
        /// </summary>
        /// <param name="id">Id do título.</param>
        /// <param name="idUsuario">Id do usuário.</param>
        /// <returns>O objeto do tipo <typeparamref name="RS"/> correspondente ao Id do título.</returns>
        Task<RS> Obter(I id, I idUsuario);

        /// <summary>
        /// Adiciona um novo título ao sistema.
        /// </summary>
        /// <param name="entidade">Objeto do tipo <typeparamref name="RQ"/> representando os dados do título.</param>
        /// <param name="idUsuario">Id do usuário.</param>
        /// <returns>O objeto do tipo <typeparamref name="RS"/> representando o título adicionado.</returns>
        Task<RS> Adicionar(RQ entidade, I idUsuario);

        /// <summary>
        /// Atualiza os dados de um título pelo seu Id.
        /// </summary>
        /// <param name="id">Id do título a ser atualizado.</param>
        /// <param name="entidade">Objeto do tipo <typeparamref name="RQ"/> com os dados atualizados.</param>
        /// <param name="idUsuario">Id do usuário.</param>
        /// <returns>O objeto do tipo <typeparamref name="RS"/> representando o título atualizado.</returns>
        Task<RS> Atualizar(I id, RQ entidade, I idUsuario);

        /// <summary>
        /// Inativa um título pelo seu Id.
        /// </summary>
        /// <param name="id">Id do título a ser inativado.</param>
        /// <param name="idUsuario">Id do usuário.</param>
        /// <returns>Uma tarefa representando a operação de inativação.</returns>
        Task Inativar(I id, I idUsuario);

        /// <summary>
        /// Obtém títulos em um período específico para um usuário.
        /// </summary>
        /// <param name="inicio">Data inicial do período.</param>
        /// <param name="fim">Data final do período.</param>
        /// <param name="idUsuario">Id do usuário.</param>
        /// <returns>Uma lista de objetos do tipo <typeparamref name="RS"/> representando os títulos no período especificado.</returns>
        Task<IEnumerable<RS>> ObterPorPeriodo(DateTime inicio, DateTime fim, I idUsuario);

        /// <summary>
        /// Obtém um título específico pelo seu Id.
        /// </summary>
        /// <param name="id">Id do título.</param>
        /// <returns>Uma lista de objetos do tipo <typeparamref name="RS"/> representando o título correspondente ao Id.</returns>
        Task<IEnumerable<RS>> ObterTituloPorId(int id);
    }
}
