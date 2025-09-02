public class MaterialEstoqueDTO
{
    public int IdMaterial { get; set; }
    public required string Nome { get; set; }
    public string? Marca { get; set; }
    public string? Modelo { get; set; }
    public int Quantidade { get; set; }
    public string? UnidadeMedida { get; set; }
    public string? Localizacao { get; set; }
    public required string CategoriaNome { get; set; }
}
