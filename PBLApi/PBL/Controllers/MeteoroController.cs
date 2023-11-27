using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PBL.Services;
using PBL.Data.Dtos;
using PBL.Models;

namespace PBL.Controllers
{
    [ApiController]
    [Route("[controller]")] // Nome do Controlador 

    public class MeteoroController : ControllerBase
    {
        private MeteoroService _service;

        public MeteoroController(MeteoroService service)
        {
            _service = service;
        }


        [HttpPost]
        public IActionResult AdicionaMeteoro([FromBody] CreateMeteoroDto MeteoroDto)
        {
            if (_service.ChecaBancoMeteoro(MeteoroDto)) 
            {
                return Conflict("Esse meteoro já existe no Banco!");
            }
            var Meteoro = _service.CreateMeteoro(MeteoroDto);
            return CreatedAtAction(nameof(RecuperaMeteorosPorId), new { Id = Meteoro.MeteoroId }, Meteoro);
        }    

        [HttpGet("{id}")]
        public IActionResult RecuperaMeteorosPorId(int id)
        {
            var resultado = _service.ReadMeteoroPorId(id);
            return resultado is null ? NotFound() : Ok(resultado);
        }
        [HttpGet()]
        public IEnumerable<ReadMeteoroDto> GetMeteoros()
        {
            return _service.ReadMeteoros();
        }
    }
}
