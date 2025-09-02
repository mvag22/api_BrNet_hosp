using System.Security.Claims;
using ApiBrnetEstoque.DTOs.Material;
using ApiBrnetEstoque.Models;
using Microsoft.EntityFrameworkCore;


namespace ApiBrnetEstoque.Services
{
    public class EstoqueService
    {
        private readonly BdBrnetEstoqueContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EstoqueService(BdBrnetEstoqueContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<MaterialEstoqueDTO>> ListarMateriais()
        {
            return await _context.MaterialEstoques
                .Include(m => m.CategoriaMaterial)
                .Select(m => new MaterialEstoqueDTO
                {
                    IdMaterial = m.IdMaterial,
                    Nome = m.Nome,
                    Marca = m.Marca,
                    Modelo = m.Modelo,
                    Quantidade = m.Quantidade,
                    UnidadeMedida = m.UnidadeMedida,
                    Localizacao = m.Localizacao,
                    CategoriaNome = m.CategoriaMaterial.Nome
                }).ToListAsync();
        }

        public async Task<MaterialEstoque> CadastrarMaterial(MaterialCreateDTO dto)
        {
            var material = new MaterialEstoque
            {
                Nome = dto.Nome,
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                Quantidade = dto.Quantidade,
                UnidadeMedida = dto.UnidadeMedida,
                Localizacao = dto.Localizacao,
                CategoriaMaterialId = dto.CategoriaMaterialId 
            };

            _context.MaterialEstoques.Add(material);
            await _context.SaveChangesAsync();
            return material;
        }


        public async Task<MaterialEstoque?> AtualizarMaterial(int id, MaterialCreateDTO dto)
        {
            var material = await _context.MaterialEstoques.FindAsync(id);
            if (material == null)
                return null;

            var quantidadeAnterior = material.Quantidade;
            var novaQuantidade = dto.Quantidade;

            // Atualiza os campos do material
            material.Nome = dto.Nome;
            material.Marca = dto.Marca;
            material.Modelo = dto.Modelo;
            material.Quantidade = novaQuantidade;
            material.UnidadeMedida = dto.UnidadeMedida;
            material.Localizacao = dto.Localizacao;
            material.CategoriaMaterialId = dto.CategoriaMaterialId;

            // Verifica se houve alteração na quantidade
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            int usuarioId = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;

            var diferenca = novaQuantidade - quantidadeAnterior;
            if (diferenca != 0)
            {
                var tipo = diferenca > 0 ? "E" : "S";

                var movimentacao = new MovimentacaoEstoque
                {
                    MaterialEstoqueId = material.IdMaterial,
                    UsuarioId = usuarioId,
                    TipoMovimentacao = tipo,
                    Quantidade = Math.Abs(diferenca),
                    UnidadeMedida = dto.UnidadeMedida,
                    Observacao = "Ajuste manual de estoque",
                    DataMovimentacao = DateTime.Now
                };

                _context.MovimentacaoEstoques.Add(movimentacao);
            }

            await _context.SaveChangesAsync();
            return material;
        }




        public async Task<bool> MovimentarEstoque(MovimentacaoEstoqueDTO dto)
        {
            var material = await _context.MaterialEstoques.FindAsync(dto.MaterialEstoqueId);
            if (material == null)
                return false;

            if (dto.TipoMovimentacao == "S" && material.Quantidade < dto.Quantidade)
                return false;

            int sinal = dto.TipoMovimentacao == "E" ? 1 : -1;
            material.Quantidade += sinal * (int)dto.Quantidade;

            var movimentacao = new MovimentacaoEstoque
            {
                MaterialEstoqueId = dto.MaterialEstoqueId,
                UsuarioId = dto.UsuarioId,
                TipoMovimentacao = dto.TipoMovimentacao,
                Quantidade = dto.Quantidade,
                UnidadeMedida = dto.UnidadeMedida,
                Observacao = dto.Observacao,
                DataMovimentacao = DateTime.Now
            };

            _context.MovimentacaoEstoques.Add(movimentacao);
            _context.MaterialEstoques.Update(material);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<List<MovimentacaoViewDTO>> ListarRelatorio()
        {
            return await _context.ViewMovimentacaoEstoqueCompleta
                .Select(v => new MovimentacaoViewDTO
                {
                    NomeMaterial = v.Material,
                    Marca = v.Marca,
                    Modelo = v.Modelo,
                    Quantidade = v.Quantidade ?? 0,
                    UnidadeMedida = v.UnidadeMedida ?? "",
                    Tecnico = v.UsuarioResponsavel,
                    DataMovimentacao = v.DataMovimentacao,
                    Observacao = v.Observacao ?? "",
                    TipoMovimentacao = v.TipoMovimentacao ?? ""
                }).ToListAsync();
        }

        public async Task<List<ViewMovimentacaoEstoqueCompletum>> ListarRelatorioPorPeriodo(DateTime inicio, DateTime fim)
        {
            var inicioDoDia = inicio.Date;
            var fimDoDia = fim.Date.AddDays(1).AddTicks(-1);

            return await _context.ViewMovimentacaoEstoqueCompleta
                .Where(v => v.DataMovimentacao >= inicioDoDia && v.DataMovimentacao <= fimDoDia)
                .ToListAsync();
        }


    }
}
