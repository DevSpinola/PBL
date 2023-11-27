using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PBL.Data;
using PBL.Data.Dtos;
using PBL.Models;

namespace PBL.Services
{
    public class ProjetilService
    {
        private IMapper _mapper;
        private PblContext _context;

        public ProjetilService(IMapper mapper, PblContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public ReadProjetilDto CreateProjetil(CreateProjetilDto dto)
        {
            ProjetilModel projetil = _mapper.Map<ProjetilModel>(dto);
            int novoProjetilId = AdicionaProjetilBanco(projetil);
            return ReadProjetilPorId(novoProjetilId);
        }
        public ReadProjetilDto? ReadProjetilPorId(int id)
        {
            ProjetilModel Projetil = _context.Projetil.FirstOrDefault(Projetil => Projetil.ProjetilId == id);
            if (Projetil != null)
            {
                ReadProjetilDto ProjetilDto = _mapper.Map<ReadProjetilDto>(Projetil);
                return ProjetilDto;
            }
            return null;
        }
        public bool ChecaBancoProjetil(CreateProjetilDto dto)
        {
            ProjetilModel? Projetil = _context.Projetil.FirstOrDefault(Projetil => Projetil.AnguloGraus.Equals(dto.AnguloGraus));
            if (Projetil is null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public int AdicionaProjetilBanco(ProjetilModel projetil)
        {
           return _context.InserirProjetil(projetil);            
        }
        public IEnumerable<ReadProjetilDto> ReadProjeteis()
        {
            return _mapper.Map<List<ReadProjetilDto>>(_context.Projetil.ToList());
        }
    }
}
