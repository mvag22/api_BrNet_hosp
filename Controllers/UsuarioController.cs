using ApiBrnetEstoque.DTOs.Usuario;
using ApiBrnetEstoque.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiBrnetEstoque.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "A,T")]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetAll([FromQuery] string? perfil = null)
        {
            var usuarios = await _service.ListarUsuarios(perfil);
            var lista = usuarios.Select(u => new UsuarioDTO
            {
                IdUsuario = u.IdUsuario,
                Nome = u.Nome
            }).ToList();

            return Ok(lista);
        }
    }
}
