using AutoMapper;
using FinTech.Api.Contract.APagar;
using FinTech.Api.Data;
using FinTech.Api.Domain.Models;
using FinTech.Api.Domain.Repository.Classes;
using FinTech.Api.Domain.Repository.Interfaces;
using FinTech.Api.Domain.Services.Interfaces;

namespace FinTech.Api.Domain.Services.Classes
{
    public class APagarService : ITituloService<APagarRequestContract, APagarResponseContract, long>
    {
        private readonly IAPagarRepository _aPagarRepository;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ApplicationContext _contexto;

        public APagarService(IAPagarRepository aPagarRepository, 
            IMapper mapper, 
            IUsuarioRepository usuarioRepository, 
            ApplicationContext contexto)
        {
            _aPagarRepository = aPagarRepository;
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            _contexto = contexto;
        }

        public async Task<APagarResponseContract> Adicionar(APagarRequestContract contrato, long idUsuario)
        {
            // Verificar se o Usuário existe
            var usuario = await _usuarioRepository.ObterPorId(idUsuario);
            
            if (usuario == null)
            {
                throw new Exception($"Usuário não encontrado com id {idUsuario}");
            }

            ValidarCamposObrigatorios(contrato);

            APagar aPagar = _mapper.Map<APagar>(contrato);
            aPagar.IdUsuario = idUsuario;

            aPagar = await _aPagarRepository.Adicionar(aPagar);

            return _mapper.Map<APagarResponseContract>(aPagar);
        }

        private void ValidarCamposObrigatorios(APagarRequestContract contrato)
        {
            string mensagemErro = string.Empty;

            switch (true)
            {
                case var _ when contrato.IdNaturezaLancamento <= 0:
                    mensagemErro = "O campo IdNaturezaLancamento é obrigatório.";
                    break;

                case var _ when contrato.ValorAPagar == null || contrato.ValorAPagar == 0:
                    mensagemErro = "O campo ValorAPagar é obrigatório e deve ser maior que zero.";
                    break;

                case var _ when contrato.ValorAPagar < 0:
                    mensagemErro = "O valor do título não pode ser negativo.";
                    break;
            }

            if (!string.IsNullOrEmpty(mensagemErro))
            {
                throw new Exception(mensagemErro);
            }
        }

        public async Task<APagarResponseContract> Atualizar(long id, APagarRequestContract contrato, long idUsuario)
        {
            APagar aPagar = await _aPagarRepository.ObterPorId(id);

            if (aPagar == null)
                throw new KeyNotFoundException("Título a pagar não encontrado.");

            _mapper.Map(contrato, aPagar);

            ValidarCamposObrigatorios(contrato);

            aPagar.Id = aPagar.Id;
            aPagar.IdUsuario = aPagar.IdUsuario;
            aPagar.DataEmissao = aPagar.DataEmissao;

            aPagar = await _aPagarRepository.Atualizar(aPagar);

            return _mapper.Map<APagarResponseContract>(aPagar);
        }

        public async Task Inativar(long id, long idUsuario)
        {
            var aPagar = await _aPagarRepository.ObterPorId(id) ?? throw new Exception("Título a pagar não encontrado para exclusão.");
            
            if (aPagar == null)
            {
                throw new Exception($"Título a pagar não encontrado com id {aPagar}");
            }
            
            await _aPagarRepository.Deletar(aPagar);
        }

        public async Task<APagarResponseContract> Obter(long id, long idUsuario)
        {
            var aPagar = await _aPagarRepository.ObterPorId(id);

            if (aPagar is null)
            {
                throw new Exception($"Não foi encontrado nenhum título a pagar pelo id {id}");
            }

            return _mapper.Map<APagarResponseContract>(aPagar);
        }

        public async Task<APagarResponseContract> ObterPorIdUsuario(long idUsuario)
        {
            var aPagar = await _aPagarRepository.ObterPorIdUsuario(idUsuario);

            if (aPagar is null || aPagar.IdUsuario != idUsuario)
            {
                throw new Exception($"Não foi encontrado nenhum título a pagar pelo id {idUsuario}");
            }

            return _mapper.Map<APagarResponseContract>(aPagar);
        }

        public async Task<IEnumerable<APagarResponseContract>> ObterTodos(long idUsuario)
        {
            var aPagar = await _aPagarRepository.ObterTodos();

            return _mapper.Map<List<APagarResponseContract>>(aPagar);
        }

        public async Task<IEnumerable<APagarResponseContract>> ObterPorPeriodo(DateTime dataInicial, DateTime dataFinal, long idUsuario)
        {
            var aPagar = await _aPagarRepository.ObterPorPeriodo(dataInicial, dataFinal, idUsuario);
            return _mapper.Map<IEnumerable<APagarResponseContract>>(aPagar);
        }

        //Mudar o nome do método na interface ITituloService e aqui.
        public async Task<IEnumerable<APagarResponseContract>> ObterTituloPorId(int id)
        {
            var aPagar = await _aPagarRepository.ObterPorId(id);
            return _mapper.Map<IEnumerable<APagarResponseContract>>(aPagar);
        }

    }
}