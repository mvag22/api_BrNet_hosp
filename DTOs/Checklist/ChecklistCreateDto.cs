using System.ComponentModel.DataAnnotations;

namespace ApiBrnetEstoque.DTOs.Checklist
{
    public class ChecklistCreateDTO
    {
        [Required]
        public int VeiculoId { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        public string? ObservacoesGerais { get; set; }

        [Required]
        public List<ChecklistItemRespostaDTO> Itens { get; set; }
    }
}
