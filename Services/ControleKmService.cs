using ApiBrnetEstoque.DTOs.ControleKm;
using ApiBrnetEstoque.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ApiBrnetEstoque.Services
{
    public class ControleKmService
    {
        private readonly BdBrnetEstoqueContext _context;

        public ControleKmService(BdBrnetEstoqueContext context)
        {
            _context = context;
        }

        public async Task<int> CriarControleKm(ControleKmCreateDTO dto)
        {
            var km = new ControleKm
            {
                Data = dto.Data,
                VeiculoId = dto.VeiculoId,
                UsuarioId1 = dto.UsuarioId1,
                UsuarioId2 = dto.UsuarioId2,
                KmInicio = dto.KmInicio,
                KmFinal = dto.KmFinal,
                Observacoes = dto.Observacoes,
                InformacoesVeridicas = dto.InformacoesVeridicas
            };

            _context.ControleKms.Add(km);
            await _context.SaveChangesAsync();

            foreach (var a in dto.Atendimentos)
            {
                var atendimento = new Atendimento
                {
                    ControleKmId = km.IdKm,
                    CodCliente = a.CodCliente,
                    NomeCliente = a.NomeCliente,
                    Observacao = a.Observacao,
                    Hora = TimeSpan.Parse(a.Hora),
                    KmAtendimento = a.Km
                };
                _context.Atendimentos.Add(atendimento);
            }

            await _context.SaveChangesAsync();
            return km.IdKm;
        }

        public async Task<List<ControleKmCompletoDTO>> ListarRelatorio(
    string? placa, DateTime? inicio, DateTime? fim, string? tecnico)
        {
            // Consulta base incluindo relacionamentos necessários
            var query = _context.ControleKms
                .Include(km => km.Atendimentos)
                .Include(km => km.Veiculo)
                .Include(km => km.UsuarioId1Navigation)   // Motorista
                .Include(km => km.UsuarioId2Navigation)   // Responsável registro
                .AsQueryable();

            if (!string.IsNullOrEmpty(placa))
                query = query.Where(km => km.Veiculo.Placa.Contains(placa));

            if (inicio.HasValue && fim.HasValue)
                query = query.Where(km => km.Data >= inicio.Value && km.Data <= fim.Value);

            if (!string.IsNullOrEmpty(tecnico))
                query = query.Where(km => km.UsuarioId1Navigation.Nome.Contains(tecnico));

            var lista = await query
                .OrderByDescending(km => km.Data)
                .ToListAsync();

            // Monta o DTO completo
            var resultado = lista.Select(km => new ControleKmCompletoDTO
            {
                IdKm = km.IdKm,
                Data = km.Data,
                Placa = km.Veiculo.Placa,
                Motorista = km.UsuarioId1Navigation.Nome,
                ResponsavelRegistro = km.UsuarioId2Navigation?.Nome ?? "",
                KmInicio = km.KmInicio,
                KmFinal = km.KmFinal,
                Observacoes = km.Observacoes,
                InformacoesVeridicas = km.InformacoesVeridicas,
                Atendimentos = km.Atendimentos.Select(a => new AtendimentoDTO
                {
                    NomeCliente = a.NomeCliente ?? "",
                    CodCliente = a.CodCliente ?? "",
                    Hora = a.Hora.HasValue ? a.Hora.Value.ToString(@"hh\:mm") : "",
                    Observacao = a.Observacao ?? "",
                    Km = a.KmAtendimento ?? 0
                }).ToList()
            }).ToList();

            return resultado;
        }

    }
}
