using AutoMapper;
using FinTech.Api.Contract.NaturezaLancamento;
using FinTech.Api.Domain.Models;
using FinTech.Api.Domain.Repository.Interfaces;
using FinTech.Api.Domain.Services.Interfaces;

namespace FinTech.Api.Domain.Services.Classes
{
    public class NaturezaLancamentoService : IService<NaturezaLancamentoRequestContract, NaturezaLancamentoResponseContract, long>
    {
        private readonly INaturezaLancamentoRepository _naturezaLancamentoRepository;
        private readonly IMapper _mapper;

        public NaturezaLancamentoService(
            INaturezaLancamentoRepository naturezaLancamentoRepository,
            IMapper mapper)
        {
            _naturezaLancamentoRepository = naturezaLancamentoRepository;
            _mapper = mapper;
        }

        public async Task<NaturezaLancamentoResponseContract> Adicionar(NaturezaLancamentoRequestContract entidade, long idUsuario)
        {
            NaturezaLancamento naturezaLancamento = _mapper.Map<NaturezaLancamento>(entidade);

            naturezaLancamento.DataCadastro = DateTime.Now;
            naturezaLancamento.IdUsuario = idUsuario;

            naturezaLancamento = await _naturezaLancamentoRepository.Adicionar(naturezaLancamento);

            return _mapper.Map<NaturezaLancamentoResponseContract>(naturezaLancamento);
        }

        public async Task<NaturezaLancamentoResponseContract> Atualizar(long id, NaturezaLancamentoRequestContract entidade, long idUsuario)
        {
            
            NaturezaLancamento naturezaLancamento = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            naturezaLancamento.Descricao = entidade.Descricao;
            naturezaLancamento.Observacao = entidade.Observacao;

            naturezaLancamento = await _naturezaLancamentoRepository.Atualizar(naturezaLancamento);

            return _mapper.Map<NaturezaLancamentoResponseContract>(naturezaLancamento);
        }

        public async Task Inativar(long id, long idUsuario)
        {
            NaturezaLancamento naturezaLancamento = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            await _naturezaLancamentoRepository.Deletar(naturezaLancamento);
        }

        public async Task<IEnumerable<NaturezaLancamentoResponseContract>> Obter(long idUsuario)
        {
            var naturezasDeLancamento = await _naturezaLancamentoRepository.ObterPorIdUsuario(idUsuario);
            return naturezasDeLancamento.Select(natureza => _mapper.Map<NaturezaLancamentoResponseContract>(natureza));
        }

        public async Task<NaturezaLancamentoResponseContract> Obter(long id, long idUsuario)
        {
            NaturezaLancamento naturezaLancamento = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);
            
            return _mapper.Map<NaturezaLancamentoResponseContract>(naturezaLancamento);
        }

        private async Task<NaturezaLancamento> ObterPorIdVinculadoAoIdUsuario(long id, long idUsuario)
        {
            var naturezaLancamento = await _naturezaLancamentoRepository.Obter(id);

            if (naturezaLancamento is null || naturezaLancamento.IdUsuario != idUsuario)
            {
                throw new Exception($"Não foi encotrada nenhuma natureza de lançamento pelo id {id}");
            }

            return naturezaLancamento;
        }
    }
}