using ApiBrnetEstoque.DTOs.Checklist;
using ApiBrnetEstoque.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiBrnetEstoque.Services
{
    public class ChecklistVeiculoService
    {
        private readonly BdBrnetEstoqueContext _context;

        public ChecklistVeiculoService(BdBrnetEstoqueContext context)
        {
            _context = context;
        }

        public async Task<int> CriarChecklistAsync(ChecklistCreateDTO dto)
        {
            var checklist = new ChecklistVeiculo
            {
                Data = DateTime.Now,
                UsuarioId = dto.UsuarioId,
                VeiculoId = dto.VeiculoId,
                Observacoes = dto.ObservacoesGerais
            };

            _context.ChecklistVeiculos.Add(checklist);
            await _context.SaveChangesAsync();

            foreach (var item in dto.Itens)
            {
                var resposta = new ChecklistRespostum
                {
                    ChecklistVeiculoId = checklist.IdChecklistVeiculo,
                    ItemId = item.ItemId,
                    Resposta = item.Resposta,
                    Observacao = item.Observacao
                };
                _context.ChecklistResposta.Add(resposta);
            }

            await _context.SaveChangesAsync();
            return checklist.IdChecklistVeiculo;
        }

        public async Task<List<ChecklistItemDTO>> ListarItensChecklist()
        {
            return await _context.ChecklistItems
                .Select(i => new ChecklistItemDTO
                {
                    IdChecklistItem = i.IdChecklistItem,
                    Nome = i.Nome
                }).ToListAsync();
        }


        public async Task<List<ChecklistRespostaCompletaDTO>> ObterChecklistCompleto()
        {
            return await _context.ViewChecklistCompletos
                .Select(v => new ChecklistRespostaCompletaDTO
                {
                    ChecklistId = v.IdChecklistVeiculo,
                    Data = v.Data,
                    VeiculoPlaca = v.Placa,
                    VeiculoModelo = v.Modelo,
                    NomeTecnico = v.Responsavel,
                    Item = v.Item,
                    Resposta = v.Resposta,
                    Observacao = v.Observacao
                }).ToListAsync();
        }
    }
}
