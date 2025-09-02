using System;
using System.Collections.Generic;

namespace ApiBrnetEstoque.Models;

public partial class Veiculo
{
    public int IdVeiculo { get; set; }

    public string Placa { get; set; } = null!;

    public string Modelo { get; set; } = null!;

    public virtual ICollection<ChecklistVeiculo> ChecklistVeiculos { get; set; } = new List<ChecklistVeiculo>();

    public virtual ICollection<ControleKm> ControleKms { get; set; } = new List<ControleKm>();
}
