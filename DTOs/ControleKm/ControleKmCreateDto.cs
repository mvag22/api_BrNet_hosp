namespace ApiBrnetEstoque.DTOs.ControleKm
{
    public class ControleKmCreateDTO
    {
        public DateTime Data { get; set; }
        public int VeiculoId { get; set; }
        public int UsuarioId1 { get; set; } // motorista
        public int? UsuarioId2 { get; set; } // acompanhante (opcional)
        public int KmInicio { get; set; }
        public int KmFinal { get; set; }
        public string? Observacoes { get; set; }
        public bool InformacoesVeridicas { get; set; }
        public List<AtendimentoCreateDTO> Atendimentos { get; set; }
    }
}
