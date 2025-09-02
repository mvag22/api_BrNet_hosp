namespace ApiBrnetEstoque.DTOs.EquipamentoReserva
{
    public class EquipamentoReservaCreateDTO
    {
        public string Mac { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }
        public int? CodCliente { get; set; }
        public string? EquipamentoAntigo { get; set; }
        public DateTime DataPegou { get; set; }
        public int UsuarioId { get; set; }
    }
}
