namespace ApiBrnetEstoque.DTOs.Checklist
{
    public class ChecklistRespostaCompletaDTO
    {
        public int ChecklistId { get; set; }
        public DateTime Data { get; set; }
        public string VeiculoPlaca { get; set; }
        public string VeiculoModelo { get; set; }
        public string NomeTecnico { get; set; }
        public string Item { get; set; }
        public string Resposta { get; set; }
        public string? Observacao { get; set; }
    }
}
