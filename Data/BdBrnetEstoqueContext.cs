using Microsoft.EntityFrameworkCore;

namespace ApiBrnetEstoque.Models;

public partial class BdBrnetEstoqueContext : DbContext
{
    public BdBrnetEstoqueContext()
    {
    }

    public BdBrnetEstoqueContext(DbContextOptions<BdBrnetEstoqueContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Atendimento> Atendimentos { get; set; }

    public virtual DbSet<CategoriaMaterial> CategoriaMaterials { get; set; }

    public virtual DbSet<ChecklistItem> ChecklistItems { get; set; }

    public virtual DbSet<ChecklistRespostum> ChecklistResposta { get; set; }

    public virtual DbSet<ChecklistVeiculo> ChecklistVeiculos { get; set; }

    public virtual DbSet<ControleKm> ControleKms { get; set; }

    public virtual DbSet<EquipamentoReserva> EquipamentoReservas { get; set; }

    public virtual DbSet<MaterialEstoque> MaterialEstoques { get; set; }

    public virtual DbSet<MovimentacaoEstoque> MovimentacaoEstoques { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Veiculo> Veiculos { get; set; }

    public virtual DbSet<ViewChecklistCompleto> ViewChecklistCompletos { get; set; }

    public virtual DbSet<ViewMovimentacaoEstoqueCompletum> ViewMovimentacaoEstoqueCompleta { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Atendimento>(entity =>
        {
            entity.HasKey(e => e.IdAtendimentos).HasName("PRIMARY");

            entity.ToTable("atendimentos");

            entity.HasIndex(e => e.ControleKmId, "controle_km_id");

            entity.Property(e => e.IdAtendimentos)
                .HasColumnType("int(11)")
                .HasColumnName("id_atendimentos");
            entity.Property(e => e.CodCliente)
                .HasMaxLength(45)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("cod_cliente");
            entity.Property(e => e.ControleKmId)
                .HasColumnType("int(11)")
                .HasColumnName("controle_km_id");
            entity.Property(e => e.Hora)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("time")
                .HasColumnName("hora");
            entity.Property(e => e.NomeCliente)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("nome_cliente");
            entity.Property(e => e.Observacao)
                .HasMaxLength(300)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("observacao");
            entity.Property(e => e.KmAtendimento)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("km_atendimento");

            entity.HasOne(d => d.ControleKm).WithMany(p => p.Atendimentos)
                .HasForeignKey(d => d.ControleKmId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("atendimentos_ibfk_1");
        });

        modelBuilder.Entity<CategoriaMaterial>(entity =>
        {
            entity.HasKey(e => e.IdCategoriaMaterial).HasName("PRIMARY");

            entity.ToTable("categoria_material");

            entity.Property(e => e.IdCategoriaMaterial)
                .HasColumnType("int(11)")
                .HasColumnName("id_categoria_material");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<ChecklistItem>(entity =>
        {
            entity.HasKey(e => e.IdChecklistItem).HasName("PRIMARY");

            entity.ToTable("checklist_item");

            entity.Property(e => e.IdChecklistItem)
                .HasColumnType("int(11)")
                .HasColumnName("id_checklist_item");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<ChecklistRespostum>(entity =>
        {
            entity.HasKey(e => e.IdChecklistResposta).HasName("PRIMARY");

            entity.ToTable("checklist_resposta");

            entity.HasIndex(e => e.ItemId, "fk_checklist_resposta_checklist_item");

            entity.HasIndex(e => e.ChecklistVeiculoId, "fk_checklist_resposta_checklist_veiculo");

            entity.Property(e => e.IdChecklistResposta)
                .HasColumnType("int(11)")
                .HasColumnName("id_checklist_resposta");
            entity.Property(e => e.ChecklistVeiculoId)
                .HasColumnType("int(11)")
                .HasColumnName("checklist_veiculo_id");
            entity.Property(e => e.ItemId)
                .HasColumnType("int(11)")
                .HasColumnName("item_id");
            entity.Property(e => e.Observacao)
                .HasMaxLength(300)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("observacao");
            entity.Property(e => e.Resposta)
                .HasMaxLength(300)
                .HasColumnName("resposta");

            entity.HasOne(d => d.ChecklistVeiculo).WithMany(p => p.ChecklistResposta)
                .HasForeignKey(d => d.ChecklistVeiculoId)
                .HasConstraintName("fk_checklist_resposta_checklist_veiculo");

            entity.HasOne(d => d.Item).WithMany(p => p.ChecklistResposta)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("fk_checklist_resposta_checklist_item");
        });

        modelBuilder.Entity<ChecklistVeiculo>(entity =>
        {
            entity.HasKey(e => e.IdChecklistVeiculo).HasName("PRIMARY");

            entity.ToTable("checklist_veiculo");

            entity.HasIndex(e => e.UsuarioId, "usuario_id");

            entity.HasIndex(e => e.VeiculoId, "veiculo_id");

            entity.Property(e => e.IdChecklistVeiculo)
                .HasColumnType("int(11)")
                .HasColumnName("id_checklist_veiculo");
            entity.Property(e => e.Data)
                .HasColumnType("date")
                .HasColumnName("data");
            entity.Property(e => e.Observacoes)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("observacoes");
            entity.Property(e => e.UsuarioId)
                .HasColumnType("int(11)")
                .HasColumnName("usuario_id");
            entity.Property(e => e.VeiculoId)
                .HasColumnType("int(11)")
                .HasColumnName("veiculo_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.ChecklistVeiculos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("checklist_veiculo_ibfk_2");

            entity.HasOne(d => d.Veiculo).WithMany(p => p.ChecklistVeiculos)
                .HasForeignKey(d => d.VeiculoId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("checklist_veiculo_ibfk_1");
        });

        modelBuilder.Entity<ControleKm>(entity =>
        {
            entity.HasKey(e => e.IdKm).HasName("PRIMARY");

            entity.ToTable("controle_km");

            entity.HasIndex(e => e.UsuarioId1, "usuario_id1");

            entity.HasIndex(e => e.UsuarioId2, "usuario_id2");

            entity.HasIndex(e => e.VeiculoId, "veiculo_id");

            entity.Property(e => e.IdKm)
                .HasColumnType("int(11)")
                .HasColumnName("id_km");
            entity.Property(e => e.Data)
                .HasColumnType("date")
                .HasColumnName("data");
            entity.Property(e => e.KmFinal)
                .HasColumnType("int(11)")
                .HasColumnName("km final");
            entity.Property(e => e.KmInicio)
                .HasColumnType("int(11)")
                .HasColumnName("km_inicio");
            entity.Property(e => e.Observacoes)
                .HasMaxLength(300)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("observacoes");
            entity.Property(e => e.InformacoesVeridicas)
                .HasColumnType("tinyint(1)")
                .HasColumnName("informacoes_veridicas");
            entity.Property(e => e.UsuarioId1)
                .HasColumnType("int(11)")
                .HasColumnName("usuario_id1");
            entity.Property(e => e.UsuarioId2)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("usuario_id2");
            entity.Property(e => e.VeiculoId)
                .HasColumnType("int(11)")
                .HasColumnName("veiculo_id");

            entity.HasOne(d => d.UsuarioId1Navigation).WithMany(p => p.ControleKmUsuarioId1Navigations)
                .HasForeignKey(d => d.UsuarioId1)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("controle_km_ibfk_2");

            entity.HasOne(d => d.UsuarioId2Navigation).WithMany(p => p.ControleKmUsuarioId2Navigations)
                .HasForeignKey(d => d.UsuarioId2)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("controle_km_ibfk_3");

            entity.HasOne(d => d.Veiculo).WithMany(p => p.ControleKms)
                .HasForeignKey(d => d.VeiculoId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("controle_km_ibfk_1");
        });

        modelBuilder.Entity<EquipamentoReserva>(entity =>
        {
            entity.HasKey(e => e.IdEquipamento).HasName("PRIMARY");

            entity.ToTable("equipamento_reserva");

            entity.HasIndex(e => e.UsuarioId, "usuario_id");

            entity.Property(e => e.IdEquipamento)
                .HasColumnType("int(11)")
                .HasColumnName("id_equipamento");
            entity.Property(e => e.CodCliente)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("cod_cliente");
            entity.Property(e => e.DataDevolucao)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date")
                .HasColumnName("data_devolucao");
            entity.Property(e => e.DataPegou)
                .HasColumnType("date")
                .HasColumnName("data_pegou");
            entity.Property(e => e.Defeito)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("defeito");
            entity.Property(e => e.Mac)
                .HasMaxLength(12)
                .HasColumnName("mac");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("status");
            entity.Property(e => e.Tipo)
                .HasMaxLength(45)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("tipo");
            entity.Property(e => e.UsuarioId)
                .HasColumnType("int(11)")
                .HasColumnName("usuario_id");
            entity.Property(e => e.EquipamentoAntigo)
                .HasMaxLength(20)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("equipamento_antigo");

            entity.HasOne(d => d.Usuario).WithMany(p => p.EquipamentoReservas)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("equipamento_reserva_ibfk_1");
        });

        modelBuilder.Entity<MaterialEstoque>(entity =>
        {
            entity.HasKey(e => e.IdMaterial).HasName("PRIMARY");

            entity.ToTable("material_estoque");

            entity.HasIndex(e => e.CategoriaMaterialId, "fk_material_categoria");

            entity.Property(e => e.IdMaterial)
                .HasColumnType("int(11)")
                .HasColumnName("id_material");
            entity.Property(e => e.CategoriaMaterialId)
                .HasColumnType("int(11)")
                .HasColumnName("categoria_material_id");
            entity.Property(e => e.Localizacao)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("localizacao");
            entity.Property(e => e.Marca)
                .HasMaxLength(45)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("marca");
            entity.Property(e => e.Modelo)
                .HasMaxLength(45)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("modelo");
            entity.Property(e => e.Nome)
                .HasMaxLength(150)
                .HasColumnName("nome");
            entity.Property(e => e.Quantidade)
                .HasColumnType("int(11)")
                .HasColumnName("quantidade");
            entity.Property(e => e.UnidadeMedida)
                .HasMaxLength(10)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("unidade_medida");

            entity.HasOne(d => d.CategoriaMaterial).WithMany(p => p.MaterialEstoques)
                .HasForeignKey(d => d.CategoriaMaterialId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_material_categoria");
        });

        modelBuilder.Entity<MovimentacaoEstoque>(entity =>
        {
            entity.HasKey(e => e.IdMovimentacao).HasName("PRIMARY");

            entity.ToTable("movimentacao_estoque");

            entity.HasIndex(e => e.MaterialEstoqueId, "material_estoque_id");

            entity.HasIndex(e => e.UsuarioId, "usuario_id");

            entity.Property(e => e.IdMovimentacao)
                .HasColumnType("int(11)")
                .HasColumnName("id_movimentacao");
            entity.Property(e => e.DataMovimentacao)
                .HasColumnType("date")
                .HasColumnName("data_movimentacao");
            entity.Property(e => e.MaterialEstoqueId)
                .HasColumnType("int(11)")
                .HasColumnName("material_estoque_id");
            entity.Property(e => e.Observacao)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("observacao");
            entity.Property(e => e.Quantidade)
                .HasPrecision(5)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("quantidade");
            entity.Property(e => e.TipoMovimentacao)
                .HasMaxLength(1)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("tipo_movimentacao");
            entity.Property(e => e.UnidadeMedida)
                .HasMaxLength(10)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("unidade_medida");
            entity.Property(e => e.UsuarioId)
                .HasColumnType("int(11)")
                .HasColumnName("usuario_id");

            entity.HasOne(d => d.MaterialEstoque).WithMany(p => p.MovimentacaoEstoques)
                .HasForeignKey(d => d.MaterialEstoqueId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("movimentacao_estoque_ibfk_2");

            entity.HasOne(d => d.Usuario).WithMany(p => p.MovimentacaoEstoques)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("movimentacao_estoque_ibfk_1");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.Login, "login").IsUnique();

            entity.Property(e => e.IdUsuario)
                .HasColumnType("int(11)")
                .HasColumnName("id_usuario");
            entity.Property(e => e.Ativo)
                .HasDefaultValueSql("'1'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("ativo");
            entity.Property(e => e.DataAtualizacao)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date")
                .HasColumnName("data_atualizacao");
            entity.Property(e => e.DataCriacao)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date")
                .HasColumnName("data_criacao");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("email");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .HasColumnName("login");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
            entity.Property(e => e.Perfil)
                .HasMaxLength(1)
                .HasColumnName("perfil");
            entity.Property(e => e.Senha)
                .HasMaxLength(80)
                .HasColumnName("senha");
        });

        modelBuilder.Entity<Veiculo>(entity =>
        {
            entity.HasKey(e => e.IdVeiculo).HasName("PRIMARY");

            entity.ToTable("veiculo");

            entity.HasIndex(e => e.Placa, "placa").IsUnique();

            entity.Property(e => e.IdVeiculo)
                .HasColumnType("int(11)")
                .HasColumnName("id_veiculo");
            entity.Property(e => e.Modelo)
                .HasMaxLength(100)
                .HasColumnName("modelo");
            entity.Property(e => e.Placa)
                .HasMaxLength(10)
                .HasColumnName("placa");
        });

        modelBuilder.Entity<ViewChecklistCompleto>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_checklist_completo");

            entity.Property(e => e.Data)
                .HasColumnType("date")
                .HasColumnName("data");
            entity.Property(e => e.IdChecklistVeiculo)
                .HasColumnType("int(11)")
                .HasColumnName("id_checklist_veiculo");
            entity.Property(e => e.Item)
                .HasMaxLength(100)
                .HasColumnName("item");
            entity.Property(e => e.Modelo)
                .HasMaxLength(100)
                .HasColumnName("modelo");
            entity.Property(e => e.Observacao)
                .HasMaxLength(300)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("observacao");
            entity.Property(e => e.Placa)
                .HasMaxLength(10)
                .HasColumnName("placa");
            entity.Property(e => e.Responsavel)
                .HasMaxLength(100)
                .HasColumnName("responsavel");
            entity.Property(e => e.Resposta)
                .HasMaxLength(300)
                .HasColumnName("resposta");
        });

        modelBuilder.Entity<ViewMovimentacaoEstoqueCompletum>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_movimentacao_estoque_completa");

            entity.Property(e => e.Categoria)
                .HasMaxLength(100)
                .HasColumnName("categoria");
            entity.Property(e => e.DataMovimentacao)
                .HasColumnType("date")
                .HasColumnName("data_movimentacao");
            entity.Property(e => e.IdMovimentacao)
                .HasColumnType("int(11)")
                .HasColumnName("id_movimentacao");
            entity.Property(e => e.Localizacao)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("localizacao");
            entity.Property(e => e.Marca)
                .HasMaxLength(45)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("marca");
            entity.Property(e => e.Material)
                .HasMaxLength(150)
                .HasColumnName("material");
            entity.Property(e => e.Modelo)
                .HasMaxLength(45)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("modelo");
            entity.Property(e => e.Observacao)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("observacao");
            entity.Property(e => e.Quantidade)
                .HasPrecision(5)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("quantidade");
            entity.Property(e => e.TipoMovimentacao)
                .HasMaxLength(1)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("tipo_movimentacao");
            entity.Property(e => e.UnidadeMedida)
                .HasMaxLength(10)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("unidade_medida");
            entity.Property(e => e.UsuarioResponsavel)
                .HasMaxLength(100)
                .HasColumnName("usuario_responsavel");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
