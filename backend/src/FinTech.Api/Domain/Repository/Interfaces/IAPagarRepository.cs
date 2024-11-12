using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinTech.Api.Domain.Models;

namespace FinTech.Api.Domain.Repository.Interfaces
{
    /// <summary>
    /// Interface de repositório para operações relacionadas à entidade APagar.
    /// Define métodos para manipulação de títulos a pagar no sistema.
    /// </summary>
    public interface IAPagarRepository
    {
        /// <summary>
        /// Adiciona um novo título a pagar ao repositório.
        /// </summary>
        /// <param name="entidade">A entidade <see cref="APagar"/> que será adicionada.</param>
        /// <returns>O objeto <see cref="APagar"/> adicionado.</returns>
        Task<APagar> Adicionar(APagar entidade);

        /// <summary>
        /// Atualiza um título a pagar existente no repositório.
        /// </summary>
        /// <param name="entidade">A entidade <see cref="APagar"/> com os dados atualizados.</param>
        /// <returns>O objeto <see cref="APagar"/> atualizado.</returns>
        Task<APagar> Atualizar(APagar entidade);

        /// <summary>
        /// Remove um título a pagar do repositório.
        /// </summary>
        /// <param name="entidade">A entidade <see cref="APagar"/> que será removida.</param>
        /// <returns>Uma tarefa representando a operação de remoção.</returns>
        Task Deletar(APagar entidade);

        /// <summary>
        /// Obtém um título a pagar pelo Id.
        /// </summary>
        /// <param name="id">O Id do título a pagar.</param>
        /// <returns>O objeto <see cref="APagar"/> correspondente ao Id.</returns>
        Task<APagar> ObterPorId(long id);

        /// <summary>
        /// Obtém todos os títulos a pagar associados a um usuário específico pelo Id do usuário.
        /// </summary>
        /// <param name="idUsuario">O Id do usuário.</param>
        /// <returns>O objeto <see cref="APagar"/> correspondente ao Id do usuário.</returns>
        Task<APagar> ObterPorIdUsuario(long idUsuario);

        /// <summary>
        /// Obtém todos os títulos a pagar cadastrados.
        /// </summary>
        /// <returns>Uma lista de objetos <see cref="APagar"/>.</returns>
        Task<IEnumerable<APagar>> ObterTodos();

        /// <summary>
        /// Obtém todos os títulos a pagar dentro de um intervalo de datas específico e associados a um usuário específico.
        /// </summary>
        /// <param name="dataInicial">A data inicial do período.</param>
        /// <param name="dataFinal">A data final do período.</param>
        /// <param name="idUsuario">O Id do usuário associado aos títulos.</param>
        /// <returns>Uma lista de objetos <see cref="APagar"/> dentro do período especificado.</returns>
        Task<IEnumerable<APagar>> ObterPorPeriodo(DateTime dataInicial, DateTime dataFinal, long idUsuario);
    }
}
