using System;
using System.Collections.Generic;

namespace ApiBrnetEstoque.Models;

public partial class ChecklistItem
{
    public int IdChecklistItem { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<ChecklistRespostum> ChecklistResposta { get; set; } = new List<ChecklistRespostum>();
}
