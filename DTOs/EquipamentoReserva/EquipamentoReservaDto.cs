namespace ApiBrnetEstoque.DTOs.EquipamentoReserva
{
    public class EquipamentoReservaDTO
    {
        public int IdEquipamento { get; set; }
        public string Mac { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }
        public int? CodCliente { get; set; }
        public string Defeito { get; set; }
        public string EquipamentoAntigo { get; set; }
        public DateTime DataPegou { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public int UsuarioId { get; set; }
        public string NomeTecnico { get; set; }
    }
}
