using System;
using System.Collections.Generic;

namespace ApiBrnetEstoque.Models;

public partial class CategoriaMaterial
{
    public int IdCategoriaMaterial { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<MaterialEstoque> MaterialEstoques { get; set; } = new List<MaterialEstoque>();
}
