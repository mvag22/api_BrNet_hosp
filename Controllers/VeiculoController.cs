using ApiBrnetEstoque.DTOs;
using ApiBrnetEstoque.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBrnetEstoque.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]

    public class VeiculoController : ControllerBase
    {
        private readonly VeiculoService _service;

        public VeiculoController(VeiculoService service)
        {
            _service = service;
        }

        // Lista todos os usuários do sistema (perfil opcional como filtro)
        [HttpGet]
        [Authorize(Roles = "T")]
        public async Task<ActionResult<IEnumerable<VeiculoDTO>>> Listar()
        {
            var lista = await _service.ListarVeiculosAsync();
            return Ok(lista);
        }
    }
}
