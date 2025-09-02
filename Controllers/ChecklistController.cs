using ApiBrnetEstoque.DTOs;
using ApiBrnetEstoque.DTOs.Checklist;
using ApiBrnetEstoque.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBrnetEstoque.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChecklistController : ControllerBase
    {
        private readonly ChecklistVeiculoService _service;
        // Construtor com injeção do serviço
        public ChecklistController(ChecklistVeiculoService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = "T")]
        public async Task<IActionResult> CriarChecklist([FromBody] ChecklistCreateDTO dto)
        {
            var id = await _service.CriarChecklistAsync(dto);
            return CreatedAtAction(nameof(ObterChecklist), new { id = id }, new { id });
        }

        [HttpGet]
        [Authorize(Roles = "A")]
        public async Task<ActionResult<IEnumerable<ChecklistRespostaCompletaDTO>>> ObterChecklist()
        {
            var lista = await _service.ObterChecklistCompleto();
            return Ok(lista);
        }

        [HttpGet("itens")]
        [Authorize(Roles = "T")]
        public async Task<ActionResult<List<ChecklistItemDTO>>> ObterItensChecklist()
        {
            var itens = await _service.ListarItensChecklist();
            return Ok(itens);
        }

    }
}
