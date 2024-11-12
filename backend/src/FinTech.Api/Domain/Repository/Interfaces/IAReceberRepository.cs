using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinTech.Api.Domain.Models;

namespace FinTech.Api.Domain.Repository.Interfaces
{
    /// <summary>
    /// Interface de repositório para operações relacionadas à entidade AReceber.
    /// Define métodos para manipulação de títulos a receber no sistema.
    /// </summary>
    public interface IAReceberRepository
    {
        /// <summary>
        /// Adiciona um novo título a receber ao repositório.
        /// </summary>
        /// <param name="entidade">A entidade <see cref="AReceber"/> que será adicionada.</param>
        /// <returns>O objeto <see cref="AReceber"/> adicionado.</returns>
        Task<AReceber> Adicionar(AReceber entidade);

        /// <summary>
        /// Atualiza um título a receber existente no repositório.
        /// </summary>
        /// <param name="entidade">A entidade <see cref="AReceber"/> com os dados atualizados.</param>
        /// <returns>O objeto <see cref="AReceber"/> atualizado.</returns>
        Task<AReceber> Atualizar(AReceber entidade);

        /// <summary>
        /// Remove um título a receber do repositório.
        /// </summary>
        /// <param name="entidade">A entidade <see cref="AReceber"/> que será removida.</param>
        /// <returns>Uma tarefa representando a operação de remoção.</returns>
        Task Deletar(AReceber entidade);

        /// <summary>
        /// Obtém um título a receber pelo Id.
        /// </summary>
        /// <param name="id">O Id do título a receber.</param>
        /// <returns>O objeto <see cref="AReceber"/> correspondente ao Id.</returns>
        Task<AReceber> ObterPorId(long id);

        /// <summary>
        /// Obtém todos os títulos a receber associados a um usuário específico pelo Id do usuário.
        /// </summary>
        /// <param name="idUsuario">O Id do usuário.</param>
        /// <returns>O objeto <see cref="AReceber"/> correspondente ao Id do usuário.</returns>
        Task<AReceber> ObterPorIdUsuario(long idUsuario);

        /// <summary>
        /// Obtém todos os títulos a receber cadastrados.
        /// </summary>
        /// <returns>Uma lista de objetos <see cref="AReceber"/>.</returns>
        Task<IEnumerable<AReceber>> ObterTodos();

        /// <summary>
        /// Obtém todos os títulos a receber dentro de um intervalo de datas específico e associados a um usuário específico.
        /// </summary>
        /// <param name="dataInicial">A data inicial do período.</param>
        /// <param name="dataFinal">A data final do período.</param>
        /// <param name="idUsuario">O Id do usuário associado aos títulos.</param>
        /// <returns>Uma lista de objetos <see cref="AReceber"/> dentro do período especificado.</returns>
        Task<IEnumerable<AReceber>> ObterPorPeriodo(DateTime dataInicial, DateTime dataFinal, long idUsuario);
    }
}
