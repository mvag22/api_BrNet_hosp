using System;
using System.Collections.Generic;

namespace ApiBrnetEstoque.Models;

public partial class ViewChecklistCompleto
{
    public int IdChecklistVeiculo { get; set; }

    public DateTime Data { get; set; }

    public string Placa { get; set; } = null!;

    public string Modelo { get; set; } = null!;

    public string Responsavel { get; set; } = null!;

    public string Item { get; set; } = null!;

    public string Resposta { get; set; } = null!;

    public string? Observacao { get; set; }
}
