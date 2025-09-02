using ApiBrnetEstoque.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiBrnetEstoque.Services
{
    public class UsuarioService
    {
        private readonly BdBrnetEstoqueContext _context;

        public UsuarioService(BdBrnetEstoqueContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> ListarUsuarios(string? perfil = null)
        {
            var query = _context.Usuarios.AsQueryable();

            if (!string.IsNullOrEmpty(perfil))
                query = query.Where(u => u.Perfil == perfil); // 'A' ou 'T'

            return await query.Where(u => u.Ativo == 1).ToListAsync(); // só usuários ativos
        }
    }
}
