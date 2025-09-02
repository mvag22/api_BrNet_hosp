namespace ApiBrnetEstoque.DTOs.Material
{
    public class MaterialUtilizadoCreateDTO
    {
        public int MaterialEstoqueId { get; set; }
        public int UsuarioId { get; set; } 
        public decimal Quantidade { get; set; }
        public string UnidadeMedida { get; set; }
        public string Observacao { get; set; }
    }
}
