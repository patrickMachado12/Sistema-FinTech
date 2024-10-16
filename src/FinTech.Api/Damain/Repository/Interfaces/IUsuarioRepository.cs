using FinTech.Api.Damain.Models;

namespace FinTech.Api.Damain.Repository.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario, long>
    {
        Task<Usuario?> Obter(string  email);
    }
}