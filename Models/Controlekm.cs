using System;
using System.Collections.Generic;

namespace ApiBrnetEstoque.Models;

public partial class ControleKm
{
    public int IdKm { get; set; }

    public DateTime Data { get; set; }

    public int VeiculoId { get; set; }

    public int UsuarioId1 { get; set; }

    public int? UsuarioId2 { get; set; }

    public int KmInicio { get; set; }

    public int KmFinal { get; set; }

    public string? Observacoes { get; set; }

    public bool InformacoesVeridicas { get; set; }

    public virtual ICollection<Atendimento> Atendimentos { get; set; } = new List<Atendimento>();

    public virtual Usuario UsuarioId1Navigation { get; set; } = null!;

    public virtual Usuario? UsuarioId2Navigation { get; set; }

    public virtual Veiculo Veiculo { get; set; } = null!;
}
