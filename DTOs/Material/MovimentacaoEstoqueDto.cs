namespace ApiBrnetEstoque.DTOs.Material
{
    public class MovimentacaoEstoqueDTO
    {
        public int MaterialEstoqueId { get; set; }
        public int UsuarioId { get; set; }
        public string TipoMovimentacao { get; set; } // E ou S
        public decimal Quantidade { get; set; }
        public string UnidadeMedida { get; set; }
        public string Observacao { get; set; }
    }
}
