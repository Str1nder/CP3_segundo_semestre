using AutoMapper;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using CP3.Domain.Interfaces.Dtos;

namespace CP3.Application.Services
{
    public class BarcoApplicationService : IBarcoApplicationService
    {
        private readonly IBarcoRepository _barcoRepository;
        private readonly IMapper _mapper;

        public BarcoApplicationService(IBarcoRepository barcoRepository, IMapper mapper)
        {
            _barcoRepository = barcoRepository;
            _mapper = mapper;
        }

        public IEnumerable<BarcoEntity> ObterTodosBarcos()
        {
            return _barcoRepository.ObterTodos() ?? new List<BarcoEntity>();
        }

        public BarcoEntity ObterBarcoPorId(int id)
        {
            var barco = _barcoRepository.ObterPorId(id);
            if (barco == null)
                throw new KeyNotFoundException($"Barco com ID {id} não encontrado.");
            return barco;
        }

        public BarcoEntity AdicionarBarco(IBarcoDto dto)
        {
            var barco = _mapper.Map<BarcoEntity>(dto);
            return _barcoRepository.Adicionar(barco);
        }

        public BarcoEntity EditarBarco(int id, IBarcoDto dto)
        {
            var barcoExistente = _barcoRepository.ObterPorId(id);
            if (barcoExistente == null)
                throw new KeyNotFoundException($"Barco com ID {id} não encontrado.");

            _mapper.Map(dto, barcoExistente);
            return _barcoRepository.Editar(barcoExistente);
        }

        public BarcoEntity RemoverBarco(int id)
        {
            var barco = _barcoRepository.Remover(id);
            if (barco == null)
                throw new KeyNotFoundException($"Barco com ID {id} não encontrado.");
            return barco;
        }
    }
}
