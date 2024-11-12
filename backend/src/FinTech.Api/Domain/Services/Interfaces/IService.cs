namespace FinTech.Api.Domain.Services.Interfaces
{
    /// <summary>
    /// Interface generica para criação de serviços do tipo CRUD.
    /// </summary>
    /// <typeparam name="RQ">Contrato de request</typeparam>
    /// <typeparam name="RS">Contrato de response</typeparam>
    /// <typeparam name="I">Tipo do Id</typeparam>
    public interface IService<RQ, RS, I> where RQ : class
    {
        /// <summary>
        /// Obtém uma lista de todas as entidades associadas ao usuário.
        /// </summary>
        /// <param name="idUsuario">Id do usuário.</param>
        Task<IEnumerable<RS>> ObterTodos(I idUsuario);

        /// <summary>
        /// Obtém uma entidade específica pelo seu Id e Id do usuário.
        /// </summary>
        /// <param name="id">Id da entidade.</param>
        /// <param name="idUsuario">Id do usuário.</param>
        Task<RS> Obter(I id, I idUsuario);

        /// <summary>
        /// Adiciona uma nova entidade ao sistema.
        /// </summary>
        /// <param name="entidade">Objeto do tipo <typeparamref name="RQ"/> representando os dados da entidade.</param>
        /// <param name="idUsuario">Id do usuário.</param>
        Task<RS> Adicionar(RQ entidade, I idUsuario);

        /// <summary>
        /// Atualiza uma entidade existente no sistema.
        /// </summary>
        /// <param name="id">Id da entidade a ser atualizada.</param>
        /// <param name="entidade">Objeto do tipo <typeparamref name="RQ"/> com os dados atualizados.</param>
        /// <param name="idUsuario">Id do usuário.</param>
        Task<RS> Atualizar(I id, RQ entidade, I idUsuario);

        /// <summary>
        /// Inativa uma entidade no sistema pelo seu Id.
        /// </summary>
        /// <param name="id">Id da entidade a ser inativada.</param>
        /// <param name="idUsuario">Id do usuário.</param>
        Task Inativar(I id, I idUsuario);
    }
}
