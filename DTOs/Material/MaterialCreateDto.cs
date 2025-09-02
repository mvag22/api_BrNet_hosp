namespace ApiBrnetEstoque.DTOs.Material
{
    public class MaterialCreateDTO
    {
        public string Nome { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Quantidade { get; set; }
        public string UnidadeMedida { get; set; }
        public string Localizacao { get; set; }
        public int CategoriaMaterialId { get; set; }
    }
}
