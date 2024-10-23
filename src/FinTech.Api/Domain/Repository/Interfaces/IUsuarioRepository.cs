using FinTech.Api.Domain.Models;

namespace FinTech.Api.Domain.Repository.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario, long>
    {
        Task<Usuario?> Obter(string  email);
    }
}