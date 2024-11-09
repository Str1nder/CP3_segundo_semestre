using CP3.Data.AppData;
using CP3.Data.Repositories;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CP3.Tests
{
    public class BarcoRepositoryTests
    {
        private readonly DbContextOptions<ApplicationContext> _options;
        private readonly ApplicationContext _context;
        private readonly BarcoRepository _BarcoRepository;

        public BarcoRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "BarcoDatabase")
                .Options;

            _context = new ApplicationContext(_options);

            _BarcoRepository = new BarcoRepository(_context);
        }

        [Fact]
        public void ObterPorId_DeveRetornarBarcoQuandoExistir()
        {
            // Arrange
            var barco = new BarcoEntity
            {
                Nome = "Barco A",
                Modelo = "Modelo A",
                Ano = 2020,
                Tamanho = 30
            };
            _context.Set<BarcoEntity>().Add(barco);
            _context.SaveChanges();

            // Act
            var resultado = _BarcoRepository.ObterPorId(barco.Id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(barco.Id, resultado?.Id);
            Assert.Equal("Barco A", resultado?.Nome);
        }

        [Fact]
        public void ObterPorId_DeveRetornarNullQuandoNaoExistir()
        {
            // Act
            var resultado = _BarcoRepository.ObterPorId(999);

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public void ObterTodos_DeveRetornarTodosOsBarcos()
        {
            // Arrange
            var barco1 = new BarcoEntity
            {
                Nome = "Barco A",
                Modelo = "Modelo A",
                Ano = 2020,
                Tamanho = 30
            };
            var barco2 = new BarcoEntity
            {
                Nome = "Barco B",
                Modelo = "Modelo B",
                Ano = 2021,
                Tamanho = 35
            };
            _context.Set<BarcoEntity>().AddRange(barco1, barco2);
            _context.SaveChanges();

            // Act
            var resultado = _BarcoRepository.ObterTodos();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.Count());
        }

        [Fact]
        public void Adicionar_DeveAdicionarUmBarco()
        {
            // Arrange
            var barco = new BarcoEntity
            {
                Nome = "Barco C",
                Modelo = "Modelo C",
                Ano = 2022,
                Tamanho = 40
            };

            // Act
            var resultado = _BarcoRepository.Adicionar(barco);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Barco C", resultado?.Nome);
            Assert.Equal(1, _context.Set<BarcoEntity>().Count());
        }

        [Fact]
        public void Remover_DeveRemoverUmBarco()
        {
            // Arrange
            var barco = new BarcoEntity
            {
                Nome = "Barco A",
                Modelo = "Modelo A",
                Ano = 2020,
                Tamanho = 30
            };
            _context.Set<BarcoEntity>().Add(barco);
            _context.SaveChanges();

            // Act
            var resultado = _BarcoRepository.Remover(barco.Id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(barco.Id, resultado?.Id);
            Assert.Equal(0, _context.Set<BarcoEntity>().Count());
        }

        [Fact]
        public void Editar_DeveAtualizarUmBarco()
        {
            // Arrange
            var barco = new BarcoEntity
            {
                Nome = "Barco A",
                Modelo = "Modelo A",
                Ano = 2020,
                Tamanho = 30
            };
            _context.Set<BarcoEntity>().Add(barco);
            _context.SaveChanges();

            _context.Entry(barco).State = EntityState.Detached;

            var barcoEditado = new BarcoEntity
            {
                Id = barco.Id,
                Nome = "Barco A Editado",
                Modelo = "Modelo A",
                Ano = 2020,
                Tamanho = 35
            };

            // Act
            var resultado = _BarcoRepository.Editar(barcoEditado);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Barco A Editado", resultado?.Nome);
            Assert.Equal(5, _context.Set<BarcoEntity>().Count());
        }


    }
}
