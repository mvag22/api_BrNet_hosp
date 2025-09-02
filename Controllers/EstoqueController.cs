using ApiBrnetEstoque.DTOs;
using ApiBrnetEstoque.DTOs.Material;
using ApiBrnetEstoque.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBrnetEstoque.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "A")]
    public class EstoqueController : ControllerBase
    {
        private readonly EstoqueService _service;
        // Construtor com injeção do serviço
        public EstoqueController(EstoqueService service)
        {
            _service = service;
        }

        // Lista todos os materiais cadastrados
        [HttpGet("materiais")]
        public async Task<IActionResult> ListarMateriais()
        {
            var lista = await _service.ListarMateriais();
            return Ok(lista);
        }

        // Cadastra novo material
        [HttpPost("materiais")]
        public async Task<IActionResult> CadastrarMaterial(MaterialCreateDTO dto)
        {
            var material = await _service.CadastrarMaterial(dto);
            return CreatedAtAction(nameof(ListarMateriais), new { id = material.IdMaterial }, material);
        }

        // Registra movimentação de estoque (entrada/saída)
        [HttpPost("movimentar")]
        public async Task<IActionResult> Movimentar(MovimentacaoEstoqueDTO dto)
        {
            var ok = await _service.MovimentarEstoque(dto);
            if (!ok) return NotFound("Material não encontrado.");
            return NoContent();
        }

        // Atualiza um material pelo ID
        [HttpPut("materiais/{id}")]
        public async Task<IActionResult> AtualizarMaterial(int id, MaterialCreateDTO dto)
        {
            var material = await _service.AtualizarMaterial(id, dto);
            if (material == null)
                return NotFound("Material não encontrado.");

            return Ok(material);
        }

        // Gera relatório geral do estoque
        [HttpGet("relatorio")]
        public async Task<IActionResult> Relatorio()
        {
            var dados = await _service.ListarRelatorio();
            return Ok(dados);
        }

        // Gera relatório do estoque por período

        [HttpGet("relatorio-periodo")]
        public async Task<IActionResult> RelatorioPorPeriodo([FromQuery] DateTime inicio, [FromQuery] DateTime fim)
        {
            var dados = await _service.ListarRelatorioPorPeriodo(inicio, fim);
            return Ok(dados);
        }

    }
}
