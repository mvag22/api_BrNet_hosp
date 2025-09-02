using ApiBrnetEstoque.DTOs.Material;
using ApiBrnetEstoque.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiBrnetEstoque.Services
{
    public class MaterialUtilizadoService
    {
        private readonly BdBrnetEstoqueContext _context;

        public MaterialUtilizadoService(BdBrnetEstoqueContext context)
        {
            _context = context;
        }

        public async Task<bool> RegistrarUtilizacao(MaterialUtilizadoCreateDTO dto)
        {
            var material = await _context.MaterialEstoques.FindAsync(dto.MaterialEstoqueId);
            if (material == null || material.Quantidade < dto.Quantidade) return false;

            material.Quantidade -= (int)dto.Quantidade;

            var mov = new MovimentacaoEstoque
            {
                MaterialEstoqueId = dto.MaterialEstoqueId,
                UsuarioId = dto.UsuarioId,
                TipoMovimentacao = "S",
                Quantidade = dto.Quantidade,
                UnidadeMedida = dto.UnidadeMedida,
                Observacao = dto.Observacao,
                DataMovimentacao = DateTime.Now
            };

            _context.MovimentacaoEstoques.Add(mov);
            _context.MaterialEstoques.Update(material);
            await _context.SaveChangesAsync();
            return true;
        }    


    }
}
