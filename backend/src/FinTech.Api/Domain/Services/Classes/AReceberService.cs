using AutoMapper;
using FinTech.Api.Contract.AReceber;
using FinTech.Api.Data;
using FinTech.Api.Domain.Models;
using FinTech.Api.Domain.Repository.Classes;
using FinTech.Api.Domain.Repository.Interfaces;
using FinTech.Api.Domain.Services.Interfaces;

namespace FinTech.Api.Domain.Services.Classes
{
    public class AReceberService : ITituloService<AReceberRequestContract, AReceberResponseContract, long>
    {
        private readonly IAReceberRepository _aReceberRepository;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ApplicationContext _contexto;

        public AReceberService(IAReceberRepository aReceberRepository, 
            IMapper mapper, 
            IUsuarioRepository usuarioRepository, 
            ApplicationContext contexto)
        {
            _aReceberRepository = aReceberRepository;
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            _contexto = contexto;
        }

        public async Task<AReceberResponseContract> Adicionar(AReceberRequestContract contrato, long idUsuario)
        {
            var usuario = await _usuarioRepository.ObterPorId(idUsuario);

            if (usuario == null)
            {
                throw new Exception($"Usuário não encontrado com id {idUsuario}");
            }

            ValidarCamposObrigatorios(contrato);

            AReceber aReceber = _mapper.Map<AReceber>(contrato);
            aReceber.IdUsuario = idUsuario;

            aReceber = await _aReceberRepository.Adicionar(aReceber);

            return _mapper.Map<AReceberResponseContract>(aReceber);
        }

        private void ValidarCamposObrigatorios(AReceberRequestContract contrato)
        {
            string mensagemErro = string.Empty;

            switch (true)
            {
                case var _ when contrato.IdNaturezaLancamento <= 0:
                    mensagemErro = "O campo IdNaturezaLancamento é obrigatório.";
                    break;

                case var _ when contrato.ValorAReceber == null || contrato.ValorAReceber == 0:
                    mensagemErro = "O campo ValorAReceber é obrigatório e deve ser maior que zero.";
                    break;

                case var _ when contrato.ValorAReceber < 0:
                    mensagemErro = "O valor do título não pode ser negativo.";
                    break;
            }

            if (!string.IsNullOrEmpty(mensagemErro))
            {
                throw new Exception(mensagemErro);
            }
        }

        public async Task<AReceberResponseContract> Atualizar(long id, AReceberRequestContract contrato, long idUsuario)
        {
            AReceber aReceber = await _aReceberRepository.ObterPorId(id);

            if (aReceber == null)
                throw new KeyNotFoundException("Título a Receber não encontrado.");

            _mapper.Map(contrato, aReceber);

            ValidarCamposObrigatorios(contrato);

            aReceber.Id = aReceber.Id;
            aReceber.IdUsuario = aReceber.IdUsuario;
            aReceber.DataEmissao = aReceber.DataEmissao;

            aReceber = await _aReceberRepository.Atualizar(aReceber);

            return _mapper.Map<AReceberResponseContract>(aReceber);
        }

        public async Task Inativar(long id, long idUsuario)
        {
            var aReceber = await _aReceberRepository.ObterPorId(id) ?? throw new Exception("Título a Receber não encontrado para exclusão.");
            
            if (aReceber == null)
            {
                throw new Exception($"Título a Receber não encontrado com id {aReceber}");
            }

            await _aReceberRepository.Deletar(aReceber);
        }

        public async Task<IEnumerable<AReceberResponseContract>> ObterTodos(long idUsuario)
        {
            var aReceber = await _aReceberRepository.ObterTodos();
            return _mapper.Map<List<AReceberResponseContract>>(aReceber);
        }

        public async Task<AReceberResponseContract> Obter(long id, long idUsuario)
        {
            var aReceber = await _aReceberRepository.ObterPorId(id);

            if (aReceber is null)
            {
                throw new Exception($"Não foi encontrado nenhum título a Receber pelo id {id}");
            }

            return _mapper.Map<AReceberResponseContract>(aReceber);
        }

        public async Task<AReceberResponseContract> ObterPorIdUsuario(long idUsuario)
        {
            var aReceber = await _aReceberRepository.ObterPorIdUsuario(idUsuario);

            if (aReceber is null || aReceber.IdUsuario != idUsuario)
            {
                throw new Exception($"Não foi encontrado nenhum título a Receber pelo id {idUsuario}");
            }

            return _mapper.Map<AReceberResponseContract>(aReceber);
        }

        public async Task<IEnumerable<AReceberResponseContract>> ObterPorPeriodo(DateTime dataInicial, DateTime dataFinal, long idUsuario)
        {
            var aReceber = await _aReceberRepository.ObterPorPeriodo(dataInicial, dataFinal, idUsuario);
            return _mapper.Map<IEnumerable<AReceberResponseContract>>(aReceber);
        }

        public async Task<IEnumerable<AReceberResponseContract>> ObterTituloPorId(int id)
        {
            var aReceber = await _aReceberRepository.ObterPorId(id);
            return _mapper.Map<IEnumerable<AReceberResponseContract>>(aReceber);
        }

    }
}