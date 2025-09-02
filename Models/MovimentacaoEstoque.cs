using System;
using System.Collections.Generic;

namespace ApiBrnetEstoque.Models;

public partial class MovimentacaoEstoque
{
    public int IdMovimentacao { get; set; }

    public int MaterialEstoqueId { get; set; }

    public int UsuarioId { get; set; }

    public DateTime DataMovimentacao { get; set; }

    public string? Observacao { get; set; }

    public string? TipoMovimentacao { get; set; }

    public decimal? Quantidade { get; set; }

    public string? UnidadeMedida { get; set; }

    public virtual MaterialEstoque MaterialEstoque { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
