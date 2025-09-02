using ApiBrnetEstoque.DTOs;
using ApiBrnetEstoque.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiBrnetEstoque.Services
{
    public class CategoriaMaterialService
    {
        private readonly BdBrnetEstoqueContext _context;

        public CategoriaMaterialService(BdBrnetEstoqueContext context)
        {
            _context = context;
        }

        public async Task<List<CategoriaMaterialDTO>> ListarCategoriasAsync()
        {
            return await _context.CategoriaMaterials
                .Select(c => new CategoriaMaterialDTO
                {
                    IdCategoriaMaterial = c.IdCategoriaMaterial,
                    Nome = c.Nome
                }).ToListAsync();
        }
    }
}
