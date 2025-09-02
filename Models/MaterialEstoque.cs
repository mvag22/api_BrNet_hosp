using System;
using System.Collections.Generic;

namespace ApiBrnetEstoque.Models;

public partial class MaterialEstoque
{
    public int IdMaterial { get; set; }

    public string Nome { get; set; } = null!;

    public string? Marca { get; set; }

    public string? Modelo { get; set; }

    public int Quantidade { get; set; }

    public string? UnidadeMedida { get; set; }

    public string? Localizacao { get; set; }

    public int CategoriaMaterialId { get; set; }

    public virtual CategoriaMaterial CategoriaMaterial { get; set; } = null!;

    public virtual ICollection<MovimentacaoEstoque> MovimentacaoEstoques { get; set; } = new List<MovimentacaoEstoque>();
}
