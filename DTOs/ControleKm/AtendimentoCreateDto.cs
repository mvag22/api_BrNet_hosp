namespace ApiBrnetEstoque.DTOs.ControleKm
{
    public class AtendimentoCreateDTO
    {
        public string Hora { get; set; }
        public string CodCliente { get; set; }
        public string NomeCliente { get; set; }
        public string? Observacao { get; set; }
        public int? Km { get; set; }
    }
}
