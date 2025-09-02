using ApiBrnetEstoque.DTOs.EquipamentoReserva;
using ApiBrnetEstoque.Models;
using ApiBrnetEstoque.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBrnetEstoque.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EquipamentoReservaController : ControllerBase
    {
        private readonly EquipamentoReservaService _service;

        // Construtor com injeção do serviço
        public EquipamentoReservaController(EquipamentoReservaService service)
        {
            _service = service;
        }

        // Lista equipamentos com filtros (Admin)
        [HttpGet]
        [Authorize(Roles = "A")]
        public async Task<ActionResult<IEnumerable<EquipamentoReservaDTO>>> GetAll(
            [FromQuery] string? mac = null,
            [FromQuery] string? status = null,
            [FromQuery] int? usuarioId = null,
            [FromQuery] DateTime? dataInicio = null,
            [FromQuery] DateTime? dataFim = null)
        {
            var lista = await _service.Listar(mac, status, usuarioId, dataInicio, dataFim);
            return Ok(lista);
        }

        // Atualiza equipamento por ID (Admin)
        [HttpPut("{id}")]
        [Authorize(Roles = "A")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] EquipamentoReservaUpdateDTO dto)
        {
            var equipamento = await _service.Atualizar(id, dto);
            if (equipamento == null)
                return NotFound();

            return NoContent();
        }

        // Cadastra novo equipamento (Admin)
        [HttpPost]
        [Authorize(Roles = "A")]
        public async Task<ActionResult> Post([FromBody] EquipamentoReservaCreateDTO dto)
        {
            var novo = await _service.Criar(dto);
            return CreatedAtAction(nameof(GetAll), new { id = novo.IdEquipamento }, novo);
        }
    }
}
