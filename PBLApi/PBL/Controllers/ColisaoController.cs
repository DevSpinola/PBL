using Microsoft.AspNetCore.Mvc;
using PBL.Services;
using PBL.Data.Dtos;


namespace PBL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ColisaoController : ControllerBase
    {
        private ColisaoService _service;


        public ColisaoController(ColisaoService ColisaoService)
        {
            _service = ColisaoService;
        }
        [HttpPost]
        public IActionResult AdicionaColisao([FromBody] CreateColisaoDto ColisaoDto)
        {
            string mensagem;
            List<ReadColisaoDto> colisoes = _service.CreateColisao(ColisaoDto, out mensagem).ToList();
            var response = new Dictionary<string, object>
            {
                 { "mensagem", mensagem },
                 { "colisoes", colisoes }
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaColisaoPorId(int id)
        {
            var resultado = _service.ReadColisaoPorId(id);
            return resultado is null ? NotFound() : Ok(resultado);
        }
        [HttpGet()]
        public IEnumerable<ReadColisaoDto> GetColisoes([FromQuery] double? anguloGraus = null)
        {
            return _service.ReadColisoes(anguloGraus);
        }

    }
}
