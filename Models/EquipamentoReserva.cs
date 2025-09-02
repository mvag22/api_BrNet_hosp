using System;
using System.Collections.Generic;

namespace ApiBrnetEstoque.Models;

public partial class EquipamentoReserva
{
    public int IdEquipamento { get; set; }

    public string Mac { get; set; } = null!;

    public string? Status { get; set; }

    public int? CodCliente { get; set; }

    public string? Defeito { get; set; }

    public DateTime DataPegou { get; set; }

    public DateTime? DataDevolucao { get; set; }

    public string? Tipo { get; set; }

    public string? EquipamentoAntigo { get; set; }

    public int UsuarioId { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
