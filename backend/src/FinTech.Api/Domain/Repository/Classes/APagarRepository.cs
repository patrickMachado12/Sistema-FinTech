using FinTech.Api.Data;
using FinTech.Api.Domain.Models;
using FinTech.Api.Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinTech.Api.Domain.Repository.Classes
{
    public class APagarRepository : IAPagarRepository
    {
        private readonly ApplicationContext _contexto;

        public APagarRepository(ApplicationContext context)
        {
            _contexto = context;
        }

        public async Task<APagar> Adicionar(APagar entidade)
        {
            await _contexto.APagar.AddAsync(entidade);
            await _contexto.SaveChangesAsync();

            return entidade;
        }

        public async Task<APagar> Atualizar(APagar entidade)
        {
            APagar entidadeBanco = await _contexto.APagar
                .Where(u => u.Id == entidade.Id)
                .FirstOrDefaultAsync();

            _contexto.Entry(entidadeBanco).CurrentValues.SetValues(entidade);
            _contexto.Update<APagar>(entidadeBanco);

            await _contexto.SaveChangesAsync();

            return entidadeBanco;
        }

        public async Task Deletar(APagar entidade)
        {
            _contexto.Entry(entidade).State = EntityState.Deleted;
           await _contexto.SaveChangesAsync();
        }

        public async Task<APagar> ObterPorId(long id)
        {
            return await _contexto.APagar.AsNoTracking()
                                                        .Where(n => n.Id == id)
                                                        .OrderBy(n => n.Id)
                                                        .FirstOrDefaultAsync();
        }

        public async Task<APagar> ObterPorIdPessoa(long idPessoa)
        {
            return await _contexto.APagar.AsNoTracking()
                                                        .Where(n => n.IdPessoa == idPessoa)
                                                        .OrderBy(n => n.Id)
                                                        .FirstOrDefaultAsync();
        }

        public async Task<APagar> ObterPorIdUsuario(long idUsuario)
        {
            return await _contexto.APagar.AsNoTracking()
                                                        .Where(n => n.IdUsuario == idUsuario)
                                                        .OrderBy(n => n.Id)
                                                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<APagar>> ObterTodos()
        {
            return await _contexto.APagar.AsNoTracking()
                                            .OrderBy(u => u.Id)
                                            .ToListAsync();
        }
    }
}