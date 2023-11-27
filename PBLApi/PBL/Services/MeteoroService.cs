using AutoMapper;
using PBL.Data.Dtos;
using PBL.Data;
using PBL.Models;

namespace PBL.Services
{
    public class MeteoroService
    {
        private IMapper _mapper;
        private PblContext _context;

        public MeteoroService(IMapper mapper, PblContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public ReadMeteoroDto CreateMeteoro(CreateMeteoroDto dto)
        {
            MeteoroModel meteoro = _mapper.Map<MeteoroModel>(dto);
            int novoMeteoroId = AdicionaMeteoroBanco(meteoro);            
            return ReadMeteoroPorId(novoMeteoroId);
        }
        public ReadMeteoroDto? ReadMeteoroPorId(int id)
        {
            MeteoroModel Meteoro = _context.Meteoro.FirstOrDefault(Meteoro => Meteoro.MeteoroId == id);
            if (Meteoro != null)
            {
                ReadMeteoroDto MeteoroDto = _mapper.Map<ReadMeteoroDto>(Meteoro);
                return MeteoroDto;
            }
            return null;
        }
        public bool ChecaBancoMeteoro(CreateMeteoroDto dto)
        {
            MeteoroModel? Meteoro = _context.Meteoro.FirstOrDefault(Meteoro => Meteoro.AlturaInicial.Equals(dto.AlturaInicial)
            && Meteoro.DistanciaHorizontal.Equals(dto.DistanciaHorizontal) && Meteoro.VelocidadeMeteoro.Equals(dto.VelocidadeMeteoro));
            if (Meteoro is null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public int AdicionaMeteoroBanco(MeteoroModel meteoro)
        {
            return _context.InserirMeteoro(meteoro);
        }

        public IEnumerable<ReadMeteoroDto> ReadMeteoros()
        {
            return _mapper.Map<List<ReadMeteoroDto>>(_context.Meteoro.ToList());
        }
    }
}
