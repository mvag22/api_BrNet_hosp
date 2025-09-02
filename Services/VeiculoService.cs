using ApiBrnetEstoque.DTOs;
using ApiBrnetEstoque.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiBrnetEstoque.Services
{
    public class VeiculoService
    {
        private readonly BdBrnetEstoqueContext _context;

        public VeiculoService(BdBrnetEstoqueContext context)
        {
            _context = context;
        }

        public async Task<List<VeiculoDTO>> ListarVeiculosAsync()
        {
            return await _context.Veiculos
                .Select(v => new VeiculoDTO
                {
                    Id = v.IdVeiculo,
                    Placa = v.Placa,
                    Modelo = v.Modelo
                }).ToListAsync();
        }
    }
}
