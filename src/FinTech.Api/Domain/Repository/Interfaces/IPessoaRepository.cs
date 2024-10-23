using FinTech.Api.Domain.Models;

namespace FinTech.Api.Domain.Repository.Interfaces
{
    public interface IPessoaRepository
    {
        Task<Pessoa> Adicionar(Pessoa entidade);
        Task<Pessoa> ObterPorId(long id);
        Task<Pessoa> ObterPorNome(string nome);
        Task<List<Pessoa>> ObterTodos();
        Task<Pessoa> Atualizar(Pessoa entidade);
        Task Deletar(Pessoa entidade);
    }
}