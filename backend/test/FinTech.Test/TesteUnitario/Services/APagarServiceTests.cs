using Xunit;
using Moq;
using FinTech.Api.Domain.Services.Classes;
using FinTech.Api.Domain.Repository.Interfaces;
using FinTech.Api.Domain.Models;
using FinTech.Api.Contract.APagar;
using AutoMapper;
using FinTech.Api.Data;

namespace FinTech.Test.TesteUnitario.Services
{
    public class APagarServiceTests
    {
        private readonly Mock<IAPagarRepository> _aPagarRepositoryMock;
        private readonly Mock<IPessoaRepository> _pessoaRepositoryMock;
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ApplicationContext> _applicationContextMock;
        private readonly APagarService _aPagarService;

        public APagarServiceTests()
        {
            _aPagarRepositoryMock = new Mock<IAPagarRepository>();
            _pessoaRepositoryMock = new Mock<IPessoaRepository>();
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            _mapperMock = new Mock<IMapper>();
            _applicationContextMock = new Mock<ApplicationContext>();
            _aPagarService = new APagarService(_aPagarRepositoryMock.Object, _mapperMock.Object, _usuarioRepositoryMock.Object, _applicationContextMock.Object);
        }

        [Fact(DisplayName = "Deve realizar o cadastro de um novo a pagar.")]
        public async Task Adicionar_DeveRetornarAPagarResponseContract_QuandoAPagarEhValido()
        {
            var aPagarRequestContract = new APagarRequestContract { IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00 };
            var aPagar = new APagar { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00 };
            var aPagarResponseContract = new APagarResponseContract { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00 };

            _mapperMock.Setup(m => m.Map<APagar>(aPagarRequestContract)).Returns(aPagar);
            _aPagarRepositoryMock.Setup(r => r.Adicionar(aPagar)).Returns(Task.FromResult(aPagar));
            _mapperMock.Setup(m => m.Map<APagarResponseContract>(aPagar)).Returns(aPagarResponseContract);

            var resultado = await _aPagarService.Adicionar(aPagarRequestContract, 1);

            Assert.NotNull(resultado);
            Assert.Equal(aPagarResponseContract.Id, resultado.Id);
            Assert.Equal(aPagarResponseContract.IdPessoa, resultado.IdPessoa);
            Assert.Equal(aPagarResponseContract.IdNaturezaLancamento, resultado.IdNaturezaLancamento);
            Assert.Equal(aPagarResponseContract.ValorAPagar, resultado.ValorAPagar);
        }

        [Fact(DisplayName = "Deve atualizar um a pagar existente.")]
        public async Task Atualizar_DeveRetornarAPagarResponseContract_QuandoAPagarEhValido()
        {
            var id = 1;
            var aPagarRequestContract = new APagarRequestContract { IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00 };
            var aPagar = new APagar { Id = id, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00 };
            var aPagarResponseContract = new APagarResponseContract { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00 };

            _mapperMock.Setup(m => m.Map<APagar>(aPagarRequestContract)).Returns(aPagar);
            _aPagarRepositoryMock.Setup(r => r.Atualizar(aPagar)).Returns(Task.FromResult(aPagar));
            _mapperMock.Setup(m => m.Map<APagarResponseContract>(aPagar)).Returns(aPagarResponseContract);

            var resultado = await _aPagarService.Atualizar(1, aPagarRequestContract, 1);

            Assert.NotNull(resultado);
            Assert.Equal(aPagarResponseContract.Id, resultado.Id);
            Assert.Equal(aPagarResponseContract.IdPessoa, resultado.IdPessoa);
            Assert.Equal(aPagarResponseContract.IdNaturezaLancamento, resultado.IdNaturezaLancamento);
            Assert.Equal(aPagarResponseContract.ValorAPagar, resultado.ValorAPagar);
        }

        [Fact(DisplayName = "Deve deletar um a pagar existente.")]
        public async Task Deletar_DeveRetornarAPagarResponseContract_QuandoAPagarEhValido()
        {
            var id = 1;
            var aPagar = new APagar { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00 };
            var aPagarResponseContract = new APagarResponseContract { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00 };

            _aPagarRepositoryMock.Setup(r => r.ObterPorId(id)).Returns(Task.FromResult(aPagar));
            _aPagarRepositoryMock.Setup(r => r.Deletar(aPagar)).Returns(Task.FromResult(true));
            _mapperMock.Setup(m => m.Map<APagarResponseContract>(aPagar)).Returns(aPagarResponseContract);

            var resultado = _aPagarService.Inativar(1, 1);

            Assert.NotNull(resultado);
            Assert.Equal(aPagarResponseContract.Id, resultado.Id);
        }

        [Fact(DisplayName = "Deve obter um a pagar por ID.")]
        public async Task ObterPorId_DeveRetornarAPagarResponseContract_QuandoAPagarEhValido()
        {
            var id = 1;
            var aPagar = new APagar { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00 };
            var aPagarResponseContract = new APagarResponseContract { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00 };

            _aPagarRepositoryMock.Setup(r => r.ObterPorId(id)).Returns(Task.FromResult(aPagar));
            _mapperMock.Setup(m => m.Map<APagarResponseContract>(aPagar)).Returns(aPagarResponseContract);

            var resultado = await _aPagarService.Obter(1, 1);

            Assert.NotNull(resultado);
            Assert.Equal(aPagarResponseContract.Id, resultado.Id);
            Assert.Equal(aPagarResponseContract.IdPessoa, resultado.IdPessoa);
            Assert.Equal(aPagarResponseContract.IdNaturezaLancamento, resultado.IdNaturezaLancamento);
            Assert.Equal(aPagarResponseContract.ValorAPagar, resultado.ValorAPagar);
        }

        [Fact(DisplayName = "Deve retornar uma lista de a pagar.")]
        public async Task ObterTodos_DeveRetornarListaDeAPagarResponseContract_QuandoAPagarExistem()
        {
            var aPagar = new List<APagar>
            {
                new APagar { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00 },
                new APagar { Id = 2, IdPessoa = 2, IdNaturezaLancamento = 2, ValorAPagar = 200.00 }
            };
            var aPagarResponseContract = new List<APagarResponseContract>
            {
                new APagarResponseContract { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00 },
                new APagarResponseContract { Id = 2, IdPessoa = 2, IdNaturezaLancamento = 2, ValorAPagar = 200.00 }
            };

            _aPagarRepositoryMock.Setup(r => r.ObterTodos()).Returns(Task.FromResult((IEnumerable<APagar>)aPagar));
            _mapperMock.Setup(m => m.Map<List<APagarResponseContract>>(aPagar)).Returns(aPagarResponseContract);

            var resultado = (await _aPagarService.ObterTodos(1)).ToList();

            Assert.NotNull(resultado);
            Assert.Equal(aPagarResponseContract.Count, resultado.Count());
            Assert.Equal(aPagarResponseContract[0].Id, resultado[0].Id);
            Assert.Equal(aPagarResponseContract[0].IdPessoa, resultado[0].IdPessoa);
            Assert.Equal(aPagarResponseContract[0].IdNaturezaLancamento, resultado[0].IdNaturezaLancamento);
            Assert.Equal(aPagarResponseContract[0].ValorAPagar, resultado[0].ValorAPagar);
            Assert.Equal(aPagarResponseContract[1].Id, resultado[1].Id);
            Assert.Equal(aPagarResponseContract[1].IdPessoa, resultado[1].IdPessoa);
            Assert.Equal(aPagarResponseContract[1].IdNaturezaLancamento, resultado[1].IdNaturezaLancamento);
            Assert.Equal(aPagarResponseContract[1].ValorAPagar, resultado[1].ValorAPagar);
        }
    }
}
       