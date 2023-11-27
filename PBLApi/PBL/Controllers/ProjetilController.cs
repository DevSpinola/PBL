using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PBL.Services;
using PBL.Data.Dtos;
using PBL.Models;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;

namespace PBL.Controllers
{
    [ApiController]
    [Route("[controller]")] // Nome do Controlador 

    public class ProjetilController : ControllerBase
    {
        private ProjetilService _service;

        public ProjetilController(ProjetilService service)
        {
            _service = service;
        }
        [HttpPost]
        public IActionResult AdicionaProjetil([FromBody] CreateProjetilDto ProjetilDto)
        {
            if (_service.ChecaBancoProjetil(ProjetilDto)) 
            {
                return Conflict("Esse projétil já existe no Banco!");
            }
            var projetil = _service.CreateProjetil(ProjetilDto);
            return CreatedAtAction(nameof(RecuperaProjetilPorId), new { Id = projetil.ProjetilId }, projetil);
        }       

        [HttpGet("{id}")]
        public IActionResult RecuperaProjetilPorId(int id)
        {
            var resultado = _service.ReadProjetilPorId(id);
            return resultado is null ? NotFound() : Ok(resultado);
        }
        [HttpGet()]
        public IEnumerable<ReadProjetilDto> GetProjetil()
        {
            return _service.ReadProjeteis();
        }

    }
}
