using AutoMapper;
using CP3.Application.Services;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using CP3.Domain.Interfaces.Dtos;
using Moq;

namespace CP3.Tests
{
    public class BarcoApplicationServiceTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IBarcoRepository> _repositoryMock;
        private readonly BarcoApplicationService _barcoService;


        public BarcoApplicationServiceTests()
        {
            _repositoryMock = new Mock<IBarcoRepository>();
            _mapperMock = new Mock<IMapper>();
            _barcoService = new BarcoApplicationService(_repositoryMock.Object, _mapperMock.Object);
        }


        [Fact]
        public void ObterBarcoPorId_DeveRetornarBarcoQuandoExistir()
        {
            // Arrange
            var barco = new BarcoEntity
            {
                Id = 1,
                Nome = "Barco A",
                Modelo = "Modelo A",
                Ano = 2020,
                Tamanho = 30
            };
            _repositoryMock.Setup(r => r.ObterPorId(1)).Returns(barco);

            // Act
            var resultado = _barcoService.ObterBarcoPorId(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.Id);
            Assert.Equal("Barco A", resultado.Nome);
        }

        [Fact]
        public void ObterBarcoPorId_DeveLancarExcecaoQuandoNaoExistir()
        {
            // Arrange
            _repositoryMock.Setup(r => r.ObterPorId(It.IsAny<int>())).Returns((BarcoEntity?)null);

            // Act
            var exception = Assert.Throws<KeyNotFoundException>(() => _barcoService.ObterBarcoPorId(999));
            
            // Assert
            Assert.Equal("Barco com ID 999 não encontrado.", exception.Message);
        }

        [Fact]
        public void AdicionarBarco_DeveAdicionarBarcoComSucesso()
        {
            // Arrange
            var barcoDto = new Mock<IBarcoDto>();
            barcoDto.Setup(d => d.Nome).Returns("Barco A");
            barcoDto.Setup(d => d.Modelo).Returns("Modelo A");
            barcoDto.Setup(d => d.Ano).Returns(2020);
            barcoDto.Setup(d => d.Tamanho).Returns(30);

            var barcoEntity = new BarcoEntity
            {
                Id = 1,
                Nome = "Barco A",
                Modelo = "Modelo A",
                Ano = 2020,
                Tamanho = 30
            };
            _repositoryMock.Setup(r => r.Adicionar(It.IsAny<BarcoEntity>())).Returns(barcoEntity);

            // Act
            var resultado = _barcoService.AdicionarBarco(barcoDto.Object);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Barco A", resultado.Nome);
            _repositoryMock.Verify(r => r.Adicionar(It.IsAny<BarcoEntity>()), Times.Once);
        }

        [Fact]
        public void EditarBarco_DeveEditarBarcoComSucesso()
        {
            // Arrange
            var barcoDto = new Mock<IBarcoDto>();
            barcoDto.Setup(d => d.Nome).Returns("Barco Editado");
            barcoDto.Setup(d => d.Modelo).Returns("Modelo A");
            barcoDto.Setup(d => d.Ano).Returns(2022);
            barcoDto.Setup(d => d.Tamanho).Returns(35);

            var barcoExistente = new BarcoEntity
            {
                Id = 1,
                Nome = "Barco A",
                Modelo = "Modelo A",
                Ano = 2020,
                Tamanho = 30
            };

            var barcoEditado = new BarcoEntity
            {
                Id = 1,
                Nome = "Barco Editado",
                Modelo = "Modelo A",
                Ano = 2022,
                Tamanho = 35
            };

            _repositoryMock.Setup(r => r.ObterPorId(1)).Returns(barcoExistente);
            _repositoryMock.Setup(r => r.Editar(It.IsAny<BarcoEntity>())).Returns(barcoEditado);

            // Act
            var resultado = _barcoService.EditarBarco(1, barcoDto.Object);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Barco Editado", resultado.Nome);
            Assert.Equal(35, resultado.Tamanho);
            _repositoryMock.Verify(r => r.Editar(It.IsAny<BarcoEntity>()), Times.Once);
        }

        [Fact]
        public void RemoverBarco_DeveRemoverBarcoComSucesso()
        {
            // Arrange
            var barco = new BarcoEntity
            {
                Id = 1,
                Nome = "Barco A",
                Modelo = "Modelo A",
                Ano = 2020,
                Tamanho = 30
            };
            _repositoryMock.Setup(r => r.ObterPorId(1)).Returns(barco);
            _repositoryMock.Setup(r => r.Remover(1)).Returns(barco);

            // Act
            var resultado = _barcoService.RemoverBarco(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.Id);
            _repositoryMock.Verify(r => r.Remover(1), Times.Once);
        }
    }
}
