using FinTech.Api.Domain.Models;
using FinTech.Api.Domain.Repository.Interfaces;
using FinTech.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace FinTech.Api.Domain.Repository.Classes
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationContext _contexto;

        public UsuarioRepository(ApplicationContext context)
        {
            _contexto = context;
        }

        public async Task<Usuario> Adicionar(Usuario entidade)
        {
            await _contexto.Usuario.AddAsync(entidade);
            await _contexto.SaveChangesAsync();

            return entidade;
        }

        public async Task<Usuario> Atualizar(Usuario entidade)
        {
            Usuario? entidadeBanco = await _contexto.Usuario
                                                .Where(p => p.Id == entidade.Id)
                                                .FirstOrDefaultAsync();

            _contexto.Entry(entidadeBanco).CurrentValues.SetValues(entidade);
            _contexto.Update<Usuario>(entidadeBanco);

            await _contexto.SaveChangesAsync();
            
            return entidadeBanco;
        }

        public async Task Deletar(Usuario entidade)
        {  
            entidade.DataInativacao = DateTime.Now;
            await _contexto.SaveChangesAsync();
        }

        public async Task<Usuario?> Obter(string email)
        {
            return await _contexto.Usuario.AsNoTracking()
                                            .Where(u => u.Email == email)
                                            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Usuario>> Obter()
        {
             return await _contexto.Usuario.AsNoTracking()
                                            .OrderBy(u => u.Id)
                                            .ToListAsync();
        }

        public async Task<Usuario?> Obter(long id)
        {
            return await _contexto.Usuario.Where(u => u.Id == id)
                                            .FirstOrDefaultAsync();
        }

        public async Task<Usuario> ObterPorId(long id)
        {
            return await _contexto.Usuario.AsNoTracking()
                                            .Where(u => u.Id == id)
                                            .FirstOrDefaultAsync();
        }

        public async Task SalvarAlteracoes()
        {
            await _contexto.SaveChangesAsync();
        }
    }
}