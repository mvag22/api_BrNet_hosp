using System;
using System.Collections.Generic;

namespace ApiBrnetEstoque.Models;

public partial class Atendimento
{
    public int IdAtendimentos { get; set; }

    public TimeSpan? Hora { get; set; }

    public string? CodCliente { get; set; }

    public string? NomeCliente { get; set; }

    public string? Observacao { get; set; }

    public int? KmAtendimento { get; set; }

    public int ControleKmId { get; set; }

    public virtual ControleKm ControleKm { get; set; } = null!;
}
