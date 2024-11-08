using Xunit;
using Moq;
using FinTech.Api.Domain.Services.Classes;
using FinTech.Api.Domain.Repository.Interfaces;
using FinTech.Api.Domain.Models;
using FinTech.Api.Contract.AReceber;
using AutoMapper;
using FinTech.Api.Data;

namespace FinTech.Test.TesteUnitario.Services
{
    public class AReceberServiceTests
    {
        private readonly Mock<IAReceberRepository> _aReceberRepositoryMock;
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ApplicationContext> _applicationContextMock;
        private readonly AReceberService _aReceberService;

        public AReceberServiceTests()
        {
            _aReceberRepositoryMock = new Mock<IAReceberRepository>();
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            _mapperMock = new Mock<IMapper>();
            _applicationContextMock = new Mock<ApplicationContext>();
            _aReceberService = new AReceberService(_aReceberRepositoryMock.Object, _mapperMock.Object, _usuarioRepositoryMock.Object, _applicationContextMock.Object);
        }

        [Fact(DisplayName = "Deve realizar o cadastro de um novo a receber.")]
        public async Task Adicionar_DeveRetornarAReceberResponseContract_QuandoAReceberEhValido()
        {
            var aReceberRequestContract = new AReceberRequestContract { IdNaturezaLancamento = 1, ValorAReceber = 100.00 };
            var aReceber = new AReceber { Id = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00 };
            var aReceberResponseContract = new AReceberResponseContract { Id = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00 };

            _mapperMock.Setup(m => m.Map<AReceber>(aReceberRequestContract)).Returns(aReceber);
            _aReceberRepositoryMock.Setup(r => r.Adicionar(aReceber)).Returns(Task.FromResult(aReceber));
            _mapperMock.Setup(m => m.Map<AReceberResponseContract>(aReceber)).Returns(aReceberResponseContract);

            var resultado = await _aReceberService.Adicionar(aReceberRequestContract, 1);

            Assert.NotNull(resultado);
            Assert.Equal(aReceberResponseContract.Id, resultado.Id);
            Assert.Equal(aReceberResponseContract.IdNaturezaLancamento, resultado.IdNaturezaLancamento);
            Assert.Equal(aReceberResponseContract.ValorAReceber, resultado.ValorAReceber);
        }

        [Fact(DisplayName = "Deve atualizar um a receber existente.")]
        public async Task Atualizar_DeveRetornarAReceberResponseContract_QuandoAReceberEhValido()
        {
            var id = 1;
            var aReceberRequestContract = new AReceberRequestContract { IdNaturezaLancamento = 1, ValorAReceber = 100.00 };
            var aReceber = new AReceber { Id = id, IdNaturezaLancamento = 1, ValorAReceber = 100.00 };
            var aReceberResponseContract = new AReceberResponseContract { Id = id, IdNaturezaLancamento = 1, ValorAReceber = 100.00 };

            _mapperMock.Setup(m => m.Map<AReceber>(aReceberRequestContract)).Returns(aReceber);
            _aReceberRepositoryMock.Setup(r => r.Atualizar(aReceber)).Returns(Task.FromResult(aReceber));
            _mapperMock.Setup(m => m.Map<AReceberResponseContract>(aReceber)).Returns(aReceberResponseContract);

            var resultado = await _aReceberService.Atualizar(1, aReceberRequestContract, 1);

            Assert.NotNull(resultado);
            Assert.Equal(aReceberResponseContract.Id, resultado.Id);
            Assert.Equal(aReceberResponseContract.IdNaturezaLancamento, resultado.IdNaturezaLancamento);
            Assert.Equal(aReceberResponseContract.ValorAReceber, resultado.ValorAReceber);
        }

        [Fact(DisplayName = "Deve deletar um a receber existente.")]
        public async Task Deletar_DeveRetornarAReceberResponseContract_QuandoAReceberEhValido()
        {
            var id = 1;
            var aReceber = new AReceber { Id = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00 };
            var aReceberResponseContract = new AReceberResponseContract { Id = id, IdNaturezaLancamento = 1, ValorAReceber = 100.00 };

            _aReceberRepositoryMock.Setup(r => r.ObterPorId(id)).Returns(Task.FromResult(aReceber));
            _aReceberRepositoryMock.Setup(r => r.Deletar(aReceber)).Returns(Task.FromResult(true));
            _mapperMock.Setup(m => m.Map<AReceberResponseContract>(aReceber)).Returns(aReceberResponseContract);

            var resultado = _aReceberService.Inativar(1, 1);

            Assert.NotNull(resultado);
            Assert.Equal(aReceberResponseContract.Id, resultado.Id);
        }

        [Fact(DisplayName = "Deve obter um a receber por ID.")]
        public async Task ObterPorId_DeveRetornarAReceberResponseContract_QuandoAReceberEhValido()
        {
            var id = 1;
            var aReceber = new AReceber { Id = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00 };
            var aReceberResponseContract = new AReceberResponseContract { Id = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00 };

            _aReceberRepositoryMock.Setup(r => r.ObterPorId(id)).Returns(Task.FromResult(aReceber));
            _mapperMock.Setup(m => m.Map<AReceberResponseContract>(aReceber)).Returns(aReceberResponseContract);

            var resultado = await _aReceberService.Obter(1, 1);

            Assert.NotNull(resultado);
            Assert.Equal(aReceberResponseContract.Id, resultado.Id);
            Assert.Equal(aReceberResponseContract.IdNaturezaLancamento, resultado.IdNaturezaLancamento);
            Assert.Equal(aReceberResponseContract.ValorAReceber, resultado.ValorAReceber);
        }

        [Fact(DisplayName = "Deve retornar uma lista de a receber.")]
        public async Task ObterTodos_DeveRetornarListaDeAReceberResponseContract_QuandoAReceberExistem()
        {
            var aReceber = new List<AReceber>
            {
                new AReceber { Id = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00 },
                new AReceber { Id = 2, IdNaturezaLancamento = 2, ValorAReceber = 200.00 }
            };
            var aReceberResponseContract = new List<AReceberResponseContract>
            {
                new AReceberResponseContract { Id = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00 },
                new AReceberResponseContract { Id = 2, IdNaturezaLancamento = 2, ValorAReceber = 200.00 }
            };

            _aReceberRepositoryMock.Setup(r => r.ObterTodos()).Returns(Task.FromResult((IEnumerable<AReceber>)aReceber));
            _mapperMock.Setup(m => m.Map<List<AReceberResponseContract>>(aReceber)).Returns(aReceberResponseContract);

            var resultado = (await _aReceberService.ObterTodos(1)).ToList();

            Assert.NotNull(resultado);
            Assert.Equal(aReceberResponseContract.Count, resultado.Count());
            Assert.Equal(aReceberResponseContract[0].Id, resultado[0].Id);
            Assert.Equal(aReceberResponseContract[0].IdNaturezaLancamento, resultado[0].IdNaturezaLancamento);
            Assert.Equal(aReceberResponseContract[0].ValorAReceber, resultado[0].ValorAReceber);
            Assert.Equal(aReceberResponseContract[1].Id, resultado[1].Id);
            Assert.Equal(aReceberResponseContract[1].IdNaturezaLancamento, resultado[1].IdNaturezaLancamento);
            Assert.Equal(aReceberResponseContract[1].ValorAReceber, resultado[1].ValorAReceber);
        }
    }
}