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
    public class MaterialUtilizadoController : ControllerBase
    {
        private readonly MaterialUtilizadoService _service;

        public MaterialUtilizadoController(MaterialUtilizadoService service)
        {
            _service = service;
        }

        // Registra material utilizado pelos técnicos
        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] MaterialUtilizadoCreateDTO dto)
        {
            try
            {
                var sucesso = await _service.RegistrarUtilizacao(dto);
                if (!sucesso)
                    return BadRequest("Material insuficiente ou inexistente.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

    }
}
