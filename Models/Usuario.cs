using System;
using System.Collections.Generic;

namespace ApiBrnetEstoque.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nome { get; set; } = null!;

    public sbyte Ativo { get; set; }

    public string Login { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public string Perfil { get; set; } = null!;

    public string? Email { get; set; }

    public DateTime? DataCriacao { get; set; }

    public DateTime? DataAtualizacao { get; set; }

    public virtual ICollection<ChecklistVeiculo> ChecklistVeiculos { get; set; } = new List<ChecklistVeiculo>();

    public virtual ICollection<ControleKm> ControleKmUsuarioId1Navigations { get; set; } = new List<ControleKm>();

    public virtual ICollection<ControleKm> ControleKmUsuarioId2Navigations { get; set; } = new List<ControleKm>();

    public virtual ICollection<EquipamentoReserva> EquipamentoReservas { get; set; } = new List<EquipamentoReserva>();

    public virtual ICollection<MovimentacaoEstoque> MovimentacaoEstoques { get; set; } = new List<MovimentacaoEstoque>();
}
