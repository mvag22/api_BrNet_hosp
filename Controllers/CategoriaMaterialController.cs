using ApiBrnetEstoque.Models;
using ApiBrnetEstoque.DTOs;
using ApiBrnetEstoque.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBrnetEstoque.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriasController : ControllerBase
    {
        private readonly CategoriaMaterialService _service;
        // Construtor com injeção do serviço
        public CategoriasController(CategoriaMaterialService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaMaterialDTO>>> Get()
        {
            var categorias = await _service.ListarCategoriasAsync();
            return Ok(categorias);
        }
    }
}
