using AutoMapper;
using PBL.Data.Dtos;
using PBL.Data;
using PBL.Models;
using PBL.Calculos;
using System.Linq;

namespace PBL.Services
{
    public class ColisaoService
    {
        private IMapper _mapper;
        private PblContext _context;

        public ColisaoService(IMapper mapper, PblContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public IEnumerable<ReadColisaoDto> CreateColisao(CreateColisaoDto dto, out string mensagem)
        {
            List<ReadColisaoDto> colisoes = new();
            bool existeNoBanco = ChecaBancoColisao(dto);
            ProjetilModel projetil = _context.Projetil.FirstOrDefault(Projetil => Projetil.ProjetilId == dto.ProjetilId);
            MeteoroModel meteoro = _context.Meteoro.FirstOrDefault(Meteoro => Meteoro.MeteoroId == dto.MeteoroId);
            mensagem = "";
            double angulo = projetil.AnguloRad;
            int x = meteoro.DistanciaHorizontal;
            int h = meteoro.AlturaInicial;
            int vm = meteoro.VelocidadeMeteoro;
            double? t1, t2, y1, y2, vo1, vo2;
            string? mov1, mov2;
            double delta = Fisica.CalcularValores(angulo, vm, x, h, out t1, out t2, out mov1, out mov2, out y1, out y2, out vo1, out vo2);

            if (delta < 0)
            {
                colisoes = (colisoes.Concat(ReadColisaoProjetilMeteoro(dto.ProjetilId, dto.MeteoroId)).ToList());

                if (!existeNoBanco)
                {
                    ColisaoModel colisao = new(y1, t1, vo1, mov1, projetil.ProjetilId, meteoro.MeteoroId);
                    int colisaoId = AdicionaColisaoBanco(colisao);
                    colisoes.Add(ReadColisaoPorId(colisaoId));
                }             
                mensagem = "O valor do ângulo foi muito baixo tente aumentar!";
            }
            else if (t1 > 0 && t2 > 0 && vo1 > 0 && vo2 > 0 && y1 > 0 && y2 > 0)// Atinge em 2 possibilidades
            {
                mensagem = "Parabéns o ângulo que você escolheu possui 2 possibilidades de acerto!";
                colisoes = (colisoes.Concat(ReadColisaoProjetilMeteoro(dto.ProjetilId, dto.MeteoroId)).ToList());
                
                if (!existeNoBanco)
                {
                    ColisaoModel colisao1 = new(y1, t1, vo1, mov1, projetil.ProjetilId, meteoro.MeteoroId);
                    ColisaoModel colisao2 = new(y2, t2, vo2, mov2, projetil.ProjetilId, meteoro.MeteoroId);
                    int colisaoId1 = AdicionaColisaoBanco(colisao1);
                    int colisaoId2 = AdicionaColisaoBanco(colisao2);
                    colisoes.Add(ReadColisaoPorId(colisaoId1));
                    colisoes.Add(ReadColisaoPorId(colisaoId2));
                }


            }
            else if (t1 > 0 && t2 < 0 && vo1 > 0 && y1 > 0)// Só atinge em uma possibilidade
            {
                mensagem = "O ângulo que você escolheu possui 1 possibilidade de acerto!";
                colisoes = (colisoes.Concat(ReadColisaoProjetilMeteoro(dto.ProjetilId, dto.MeteoroId)).ToList());

                if (!existeNoBanco)
                {
                    ColisaoModel colisao = new(y1, t1, vo1, mov1, projetil.ProjetilId, meteoro.MeteoroId);
                    int colisaoId = AdicionaColisaoBanco(colisao);
                    colisoes.Add(ReadColisaoPorId(colisaoId));
                }                

            }
            else if (t1 < 0 && t2 > 0 && vo2 > 0 && y2 > 0)// Só atinge em uma possibilidade
            {
                mensagem = "O ângulo que você escolheu possui 1 possibilidade de acerto!";
                colisoes = (colisoes.Concat(ReadColisaoProjetilMeteoro(dto.ProjetilId, dto.MeteoroId)).ToList());

                if (!existeNoBanco)
                {
                    ColisaoModel colisao = new(y2, t2, vo2, mov2, projetil.ProjetilId, meteoro.MeteoroId);
                    int colisaoId = AdicionaColisaoBanco(colisao);
                    colisoes.Add(ReadColisaoPorId(colisaoId));
                }
            }
            else// Altura Tempo ou Velocidade menores que zero, impossibilidade de atingir
            {
                colisoes = (colisoes.Concat(ReadColisaoProjetilMeteoro(dto.ProjetilId, dto.MeteoroId)).ToList());

                if (!existeNoBanco)
                {
                    ColisaoModel colisao = new(y1, t1, vo1, mov1, projetil.ProjetilId, meteoro.MeteoroId);
                    int colisaoId = AdicionaColisaoBanco(colisao);
                    colisoes.Add(ReadColisaoPorId(colisaoId));
                }
                mensagem = "O valor do ângulo foi muito alto tente diminuir!";
            }
            return _mapper.Map<List<ReadColisaoDto>>(colisoes);
        }
        public ReadColisaoDto? ReadColisaoPorId(int id)
        {
            ColisaoModel Colisao = _context.Colisao.FirstOrDefault(Colisao => Colisao.ColisaoId == id);
            if (Colisao != null)
            {
                ReadColisaoDto ColisaoDto = _mapper.Map<ReadColisaoDto>(Colisao);
                return ColisaoDto;
            }
            return null;
        }
        public IEnumerable<ReadColisaoDto?> ReadColisaoProjetilMeteoro(int projetilId, int meteoroId)
        {
            List<ColisaoModel> colisoes = _context.Colisao.Where(Colisao => Colisao.ProjetilId == projetilId && Colisao.MeteoroId == meteoroId).ToList();            
            if (colisoes != null)
            {
                List<ReadColisaoDto> readColisoes = _mapper.Map<List<ReadColisaoDto>>(colisoes);
                return readColisoes;
            }
            return null;
        }
        public bool ChecaBancoColisao(CreateColisaoDto dto)
        {
            ColisaoModel? colisao = _context.Colisao.FirstOrDefault(colisao => colisao.ProjetilId.Equals(dto.ProjetilId) && colisao.MeteoroId.Equals(dto.MeteoroId));
            if (colisao is null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public int AdicionaColisaoBanco(ColisaoModel colisao)
        {
           return _context.InserirColisao(colisao);            
        }

        public IEnumerable<ReadColisaoDto> ReadColisoes(double? anguloGraus = null)
        {
            return anguloGraus is null ? _mapper.Map<List<ReadColisaoDto>>(_context.Colisao.ToList()) : _mapper.Map<List<ReadColisaoDto>>(_context.Colisao.Where(colisao => colisao.Projetil.AnguloGraus.Equals(anguloGraus)).ToList());
        }
    }
}
