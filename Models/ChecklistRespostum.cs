using System;
using System.Collections.Generic;

namespace ApiBrnetEstoque.Models;

public partial class ChecklistRespostum
{
    public int IdChecklistResposta { get; set; }

    public int ChecklistVeiculoId { get; set; }

    public int ItemId { get; set; }

    public string Resposta { get; set; } = null!;

    public string? Observacao { get; set; }

    public virtual ChecklistVeiculo ChecklistVeiculo { get; set; } = null!;

    public virtual ChecklistItem Item { get; set; } = null!;
}
