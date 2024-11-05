using Xunit;
using Moq;
using FinTech.Api.Domain.Services.Classes;
using FinTech.Api.Domain.Repository.Interfaces;
using FinTech.Api.Domain.Models;
using FinTech.Api.Contract.NaturezaLancamento;
using AutoMapper;

namespace FinTech.Test.TesteUnitario.Services
{
    public class NaturezaLancamentoServiceTests
    {
        private Mock<IRepository<NaturezaLancamento, int>> _naturezaLancamentoRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<INaturezaLancamentoRepository> _naturezaLancamentoRepositoryMockSpecific;
        private readonly NaturezaLancamentoService _naturezaLancamentoService;

        public NaturezaLancamentoServiceTests()
        {
            _naturezaLancamentoRepositoryMock = new Mock<IRepository<NaturezaLancamento, int>>();
            _mapperMock = new Mock<IMapper>();
            _naturezaLancamentoService = new NaturezaLancamentoService(_naturezaLancamentoRepositoryMockSpecific.Object, _mapperMock.Object);
        }

        [Fact(DisplayName = "Deve realizar o cadastro de uma nova natureza de lançamento.")]
        public async Task Adicionar_DeveRetornarNaturezaLancamentoResponseContract_QuandoNaturezaLancamentoEhValida()
        {
            var naturezaLancamentoRequestContract = new NaturezaLancamentoRequestContract { Descricao = "Lançamento de teste" };
            var naturezaLancamento = new NaturezaLancamento { Id = 1, Descricao = "Lançamento de teste" };
            var naturezaLancamentoResponseContract = new NaturezaLancamentoResponseContract { Id = 1, Descricao = "Lançamento de teste" };

            _mapperMock.Setup(m => m.Map<NaturezaLancamento>(naturezaLancamentoRequestContract)).Returns(naturezaLancamento);
            _naturezaLancamentoRepositoryMock.Setup(r => r.Adicionar(naturezaLancamento)).Returns(Task.FromResult(naturezaLancamento));
            _mapperMock.Setup(m => m.Map<NaturezaLancamentoResponseContract>(naturezaLancamento)).Returns(naturezaLancamentoResponseContract);

            var resultado = await _naturezaLancamentoService.Adicionar(naturezaLancamentoRequestContract, 1);

            Assert.NotNull(resultado);
            Assert.Equal(naturezaLancamentoResponseContract.Id, resultado.Id);
            Assert.Equal(naturezaLancamentoResponseContract.Descricao, resultado.Descricao);
        }

        [Fact(DisplayName = "Deve atualizar uma natureza de lançamento existente.")]
        public async Task Atualizar_DeveRetornarNaturezaLancamentoResponseContract_QuandoNaturezaLancamentoEhValida()
        {
            var naturezaLancamentoRequestContract = new NaturezaLancamentoRequestContract {Descricao = "Lançamento de teste atualizado" };
            var naturezaLancamento = new NaturezaLancamento { Id = 1, Descricao = "Lançamento de teste atualizado" };
            var naturezaLancamentoResponseContract = new NaturezaLancamentoResponseContract { Id = 1, Descricao = "Lançamento de teste atualizado" };

            _mapperMock.Setup(m => m.Map<NaturezaLancamento>(naturezaLancamentoRequestContract)).Returns(naturezaLancamento);
            _naturezaLancamentoRepositoryMock.Setup(r => r.Atualizar(naturezaLancamento)).Returns(Task.FromResult(naturezaLancamento));
            _mapperMock.Setup(m => m.Map<NaturezaLancamentoResponseContract>(naturezaLancamento)).Returns(naturezaLancamentoResponseContract);

            var resultado = await _naturezaLancamentoService.Atualizar(1, naturezaLancamentoRequestContract, 1);

            Assert.NotNull(resultado);
            Assert.Equal(naturezaLancamentoResponseContract.Id, resultado.Id);
            Assert.Equal(naturezaLancamentoResponseContract.Descricao, resultado.Descricao);
        }

        [Fact(DisplayName = "Deve deletar uma natureza de lançamento existente.")]
        public async Task Deletar_DeveRetornarNaturezaLancamentoResponseContract_QuandoNaturezaLancamentoEhValida()
        {
            var id = 1;
            var naturezaLancamento = new NaturezaLancamento { Id = 1, Descricao = "Lançamento de teste" };
            var naturezaLancamentoResponseContract = new NaturezaLancamentoResponseContract { Id = 1, Descricao = "Lançamento de teste" };

            _naturezaLancamentoRepositoryMockSpecific.Setup(r => ((IRepository<NaturezaLancamento, int>)r).Obter(id)).Returns(Task.FromResult(naturezaLancamento));

            _naturezaLancamentoRepositoryMock.Setup(r => ((IRepository<NaturezaLancamento, int>)r).Deletar(naturezaLancamento)).Returns(Task.FromResult(true));

            _mapperMock.Setup(m => m.Map<NaturezaLancamentoResponseContract>(naturezaLancamento)).Returns(naturezaLancamentoResponseContract);

            var resultado =  _naturezaLancamentoService.Inativar(1, 1);

            Assert.NotNull(resultado);
            Assert.Equal(naturezaLancamentoResponseContract.Id, resultado.Id);
            
        }

        [Fact(DisplayName = "Deve obter uma natureza de lançamento por ID.")]
        public async Task ObterPorId_DeveRetornarNaturezaLancamentoResponseContract_QuandoNaturezaLancamentoEhValida()
        {
            var id = 1;
            var naturezaLancamento = new NaturezaLancamento { Id = 1, Descricao = "Lançamento de teste" };
            var naturezaLancamentoResponseContract = new NaturezaLancamentoResponseContract { Id = 1, Descricao = "Lançamento de teste" };

            _naturezaLancamentoRepositoryMock.Setup(r => r.Obter(id)).Returns(Task.FromResult(naturezaLancamento));
            _mapperMock.Setup(m => m.Map<NaturezaLancamentoResponseContract>(naturezaLancamento)).Returns(naturezaLancamentoResponseContract);

            var resultado = await _naturezaLancamentoService.Obter(1, 1);

            Assert.NotNull(resultado);
            Assert.Equal(naturezaLancamentoResponseContract.Id, resultado.Id);
            Assert.Equal(naturezaLancamentoResponseContract.Descricao, resultado.Descricao);
        }

        [Fact(DisplayName = "Deve retornar uma lista de naturezas de lançamento.")]
        public async Task ObterTodos_DeveRetornarListaDeNaturezaLancamentoResponseContract_QuandoNaturezasLancamentoExistem()
        {
            var naturezasLancamento = new List<NaturezaLancamento>
            {
                new NaturezaLancamento { Id = 1, Descricao = "Lançamento de teste" },
                new NaturezaLancamento { Id = 2, Descricao = "Lançamento de teste 2" }
            };

            var naturezasLancamentoResponseContracts = new List<NaturezaLancamentoResponseContract>
            {
                new NaturezaLancamentoResponseContract { Id = 1, Descricao = "Lançamento de teste" },
                new NaturezaLancamentoResponseContract { Id = 2, Descricao = "Lançamento de teste 2" }
            };

            _naturezaLancamentoRepositoryMock.Setup(r => r.Obter()).Returns(Task.FromResult((IEnumerable<NaturezaLancamento>)naturezasLancamento));
            _mapperMock.Setup(m => m.Map<List<NaturezaLancamentoResponseContract>>(naturezasLancamento)).Returns(naturezasLancamentoResponseContracts);
            
            var resultado = await _naturezaLancamentoService.Obter(1, 2);
            var listaResultado = new List<NaturezaLancamentoResponseContract> { resultado };

            Assert.NotNull(listaResultado);
            Assert.Equal(naturezasLancamentoResponseContracts.Count, listaResultado.Count());
            Assert.Equal(naturezasLancamentoResponseContracts[0].Id, listaResultado[0].Id);
            Assert.Equal(naturezasLancamentoResponseContracts[0].Descricao, listaResultado[0].Descricao);
        }
    }
}