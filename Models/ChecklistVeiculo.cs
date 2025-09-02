using System;
using System.Collections.Generic;

namespace ApiBrnetEstoque.Models;

public partial class ChecklistVeiculo
{
    public int IdChecklistVeiculo { get; set; }

    public DateTime Data { get; set; }

    public int VeiculoId { get; set; }

    public int UsuarioId { get; set; }

    public string? Observacoes { get; set; }

    public virtual ICollection<ChecklistRespostum> ChecklistResposta { get; set; } = new List<ChecklistRespostum>();

    public virtual Usuario Usuario { get; set; } = null!;

    public virtual Veiculo Veiculo { get; set; } = null!;
}
