using ApiBrnetEstoque.DTOs;
using ApiBrnetEstoque.DTOs.ControleKm;
using ApiBrnetEstoque.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBrnetEstoque.Controllers
{
    [ApiController] // Indica que é um controller de API REST
    [Route("api/[controller]")] // Define o prefixo das rotas como api/ControleKm
    [Authorize] // Exige autenticação para acessar os métodos
    public class ControleKmController : ControllerBase
    {
        private readonly ControleKmService _service;

        // Construtor com injeção do serviço
        public ControleKmController(ControleKmService service)
        {
            _service = service;
        }

        // Registrar controle de KM (Apenas Técnicos)
        [HttpPost]
        [Authorize(Roles = "T")]
        public async Task<IActionResult> Registrar([FromBody] ControleKmCreateDTO dto)
        {
            var id = await _service.CriarControleKm(dto);
            return CreatedAtAction(nameof(Relatorio), new { id }, new { id });
        }

        // Gera relatório de KM filtrado por placa, data e técnico (Apenas Admins)
        [HttpGet("relatorio")]
        [Authorize(Roles = "A")]
        public async Task<IActionResult> Relatorio(
            [FromQuery] string? placa,
            [FromQuery] DateTime? inicio,
            [FromQuery] DateTime? fim,
            [FromQuery] string? tecnico)
        {
            var relatorio = await _service.ListarRelatorio(placa, inicio, fim, tecnico);
            return Ok(relatorio);
        }
    }

}
