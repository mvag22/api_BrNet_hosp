using ApiBrnetEstoque.DTOs.EquipamentoReserva;
using ApiBrnetEstoque.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiBrnetEstoque.Services
{
    public class EquipamentoReservaService
    {
        private readonly BdBrnetEstoqueContext _context;

        public EquipamentoReservaService(BdBrnetEstoqueContext context)
        {
            _context = context;
        }

        public async Task<List<EquipamentoReservaDTO>> Listar(string? mac = null, string? status = null, int? usuarioId = null, DateTime? dataInicio = null, DateTime? dataFim = null)
        {
            var query = _context.EquipamentoReservas
                .Include(e => e.Usuario)
                .AsQueryable();

            if (!string.IsNullOrEmpty(mac))
                query = query.Where(e => e.Mac.Contains(mac));

            if (!string.IsNullOrEmpty(status))
                query = query.Where(e => e.Status == status);

            if (usuarioId.HasValue)
                query = query.Where(e => e.UsuarioId == usuarioId.Value);

            if (dataInicio.HasValue && dataFim.HasValue)
                query = query.Where(e => e.DataPegou >= dataInicio && e.DataPegou <= dataFim);

            var lista = await query.ToListAsync();

            return lista.Select(e => new EquipamentoReservaDTO
            {
                IdEquipamento = e.IdEquipamento,
                Mac = e.Mac,
                Tipo = e.Tipo,
                Status = e.Status,
                CodCliente = e.CodCliente,
                Defeito = e.Defeito,
                EquipamentoAntigo = e.EquipamentoAntigo,
                DataPegou = e.DataPegou,
                DataDevolucao = e.DataDevolucao,
                UsuarioId = e.UsuarioId,
                NomeTecnico = e.Usuario?.Nome
            }).ToList();
        }

        public async Task<EquipamentoReserva> Criar(EquipamentoReservaCreateDTO dto)
        {
            var entity = new EquipamentoReserva
            {
                Mac = dto.Mac,
                Tipo = dto.Tipo,
                Status = dto.Status,
                CodCliente = dto.CodCliente,
                EquipamentoAntigo = dto.EquipamentoAntigo,
                DataPegou = dto.DataPegou,
                UsuarioId = dto.UsuarioId
            };

            _context.EquipamentoReservas.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        public async Task<EquipamentoReserva?> Atualizar(int id, EquipamentoReservaUpdateDTO dto)
        {
            var equipamento = await _context.EquipamentoReservas.FindAsync(id);
            if (equipamento == null) return null;

            equipamento.Mac = dto.Mac;
            equipamento.Tipo = dto.Tipo;
            equipamento.Status = dto.Status;
            equipamento.CodCliente = dto.CodCliente;
            equipamento.Defeito = dto.Defeito;
            equipamento.EquipamentoAntigo = dto.EquipamentoAntigo;
            equipamento.DataPegou = dto.DataPegou;
            equipamento.DataDevolucao = dto.DataDevolucao;
            equipamento.UsuarioId = dto.UsuarioId;

            _context.EquipamentoReservas.Update(equipamento);
            await _context.SaveChangesAsync();
            return equipamento;
        }
    }
}
