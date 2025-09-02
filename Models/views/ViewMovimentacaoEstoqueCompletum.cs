using System;
using System.Collections.Generic;

namespace ApiBrnetEstoque.Models;

public partial class ViewMovimentacaoEstoqueCompletum
{
    public int IdMovimentacao { get; set; }

    public DateTime DataMovimentacao { get; set; }

    public string? TipoMovimentacao { get; set; }

    public decimal? Quantidade { get; set; }

    public string? UnidadeMedida { get; set; }

    public string? Observacao { get; set; }

    public string UsuarioResponsavel { get; set; } = null!;

    public string Material { get; set; } = null!;

    public string? Marca { get; set; }

    public string? Modelo { get; set; }

    public string? Localizacao { get; set; }

    public string Categoria { get; set; } = null!;
}
