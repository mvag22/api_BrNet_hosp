namespace ApiBrnetEstoque.DTOs.Material
{
    public class MovimentacaoViewDTO
    {
        public string NomeMaterial { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public decimal Quantidade { get; set; }
        public string UnidadeMedida { get; set; }
        public string Tecnico { get; set; }
        public DateTime DataMovimentacao { get; set; }
        public string Observacao { get; set; }
        public string TipoMovimentacao { get; set; }  
    }
}
