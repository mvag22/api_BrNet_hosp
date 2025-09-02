namespace ApiBrnetEstoque.DTOs.ControleKm
{
    public class ControleKmCompletoDTO
    {
        public int IdKm { get; set; }
        public DateTime Data { get; set; }
        public string Placa { get; set; }
        public string Motorista { get; set; }
        public string ResponsavelRegistro { get; set; }
        public int KmInicio { get; set; }
        public int KmFinal { get; set; }
        public string Observacoes { get; set; }
        public bool InformacoesVeridicas { get; set; }
        public List<AtendimentoDTO> Atendimentos { get; set; }
    }

    public class AtendimentoDTO
    {
        public string NomeCliente { get; set; }
        public string CodCliente { get; set; }
        public string Hora { get; set; }
        public string Observacao { get; set; }
        public int Km { get; set; }
    }
}
