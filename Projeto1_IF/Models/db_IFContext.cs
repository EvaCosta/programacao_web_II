﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Projeto1_IF.Models;

public partial class db_IFContext : DbContext
{
    public db_IFContext(DbContextOptions<db_IFContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }

    public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }

    public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }

    public virtual DbSet<TbAlimento> TbAlimento { get; set; }

    public virtual DbSet<TbAntropometria> TbAntropometria { get; set; }

    public virtual DbSet<TbCategoriaMedicamento> TbCategoriaMedicamento { get; set; }

    public virtual DbSet<TbCidade> TbCidade { get; set; }

    public virtual DbSet<TbContrato> TbContrato { get; set; }

    public virtual DbSet<TbEscalaBristol> TbEscalaBristol { get; set; }

    public virtual DbSet<TbEscalaBristolPacienteConsulta> TbEscalaBristolPacienteConsulta { get; set; }

    public virtual DbSet<TbEstado> TbEstado { get; set; }

    public virtual DbSet<TbExame> TbExame { get; set; }

    public virtual DbSet<TbExameFisico> TbExameFisico { get; set; }

    public virtual DbSet<TbExameXPacientes> TbExameXPacientes { get; set; }

    public virtual DbSet<TbGrupoPatologico> TbGrupoPatologico { get; set; }

    public virtual DbSet<TbGrupoPatologicoXPatologia> TbGrupoPatologicoXPatologia { get; set; }

    public virtual DbSet<TbGruposReceitasDespesas> TbGruposReceitasDespesas { get; set; }

    public virtual DbSet<TbHistoriaPatologica> TbHistoriaPatologica { get; set; }

    public virtual DbSet<TbHistoricoAlimentarNutricional> TbHistoricoAlimentarNutricional { get; set; }

    public virtual DbSet<TbHistoricoDoencaAtual> TbHistoricoDoencaAtual { get; set; }

    public virtual DbSet<TbHistoricoSocialAlimentar> TbHistoricoSocialAlimentar { get; set; }

    public virtual DbSet<TbHoraPacienteProfissional> TbHoraPacienteProfissional { get; set; }

    public virtual DbSet<TbLancarReceitasDespesas> TbLancarReceitasDespesas { get; set; }

    public virtual DbSet<TbMedicamento> TbMedicamento { get; set; }

    public virtual DbSet<TbMedicoPaciente> TbMedicoPaciente { get; set; }

    public virtual DbSet<TbPaciente> TbPaciente { get; set; }

    public virtual DbSet<TbPais> TbPais { get; set; }

    public virtual DbSet<TbPatologia> TbPatologia { get; set; }

    public virtual DbSet<TbPergunta> TbPergunta { get; set; }

    public virtual DbSet<TbPlano> TbPlano { get; set; }

    public virtual DbSet<TbProfissional> TbProfissional { get; set; }

    public virtual DbSet<TbRastreamentoMetabolico> TbRastreamentoMetabolico { get; set; }

    public virtual DbSet<TbRastreamentoResposta> TbRastreamentoResposta { get; set; }

    public virtual DbSet<TbReceitaAlimentarPadrao> TbReceitaAlimentarPadrao { get; set; }

    public virtual DbSet<TbReceitaAlimentarPadraoXAlimento> TbReceitaAlimentarPadraoXAlimento { get; set; }

    public virtual DbSet<TbReceitaMedicaPadrao> TbReceitaMedicaPadrao { get; set; }

    public virtual DbSet<TbSubstancia> TbSubstancia { get; set; }

    public virtual DbSet<TbSuplemento> TbSuplemento { get; set; }

    public virtual DbSet<TbTipoAcesso> TbTipoAcesso { get; set; }

    public virtual DbSet<TbTipoProfissional> TbTipoProfissional { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRoles>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");
        });

        modelBuilder.Entity<AspNetUsers>(entity =>
        {
            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.HasMany(d => d.Role).WithMany(p => p.User)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRoles",
                    r => r.HasOne<AspNetRoles>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUsers>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<TbAlimento>(entity =>
        {
            entity.HasKey(e => e.IdAlimento).HasName("PK__tbAlimen__2406570577A5EC2C");
        });

        modelBuilder.Entity<TbAntropometria>(entity =>
        {
            entity.HasKey(e => e.IdAntropometria).HasName("PK__tbAntrop__53A9E5941381DBE2");

            entity.HasOne(d => d.IdHoraPacienteProfissionalNavigation).WithMany(p => p.TbAntropometria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbAntropo__IdHor__02FC7413");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.TbAntropometria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbAntropo__IdPac__03F0984C");
        });

        modelBuilder.Entity<TbCategoriaMedicamento>(entity =>
        {
            entity.HasKey(e => e.IdCategoriaMedicamento).HasName("PK__tbCatego__14A80672AD9984E8");
        });

        modelBuilder.Entity<TbCidade>(entity =>
        {
            entity.HasKey(e => e.IdCidade).HasName("PK__tbCidade__160879A3F9A62446");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.TbCidade).HasConstraintName("FK__tbCidade__IdEsta__3C69FB99");
        });

        modelBuilder.Entity<TbContrato>(entity =>
        {
            entity.HasKey(e => e.IdContrato).HasName("PK__tbContra__8569F05AC291EB56");

            entity.HasOne(d => d.IdPlanoNavigation).WithMany(p => p.TbContrato)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbContrat__IdPla__412EB0B6");
        });

        modelBuilder.Entity<TbEscalaBristol>(entity =>
        {
            entity.HasKey(e => e.IdEscalaBristol).HasName("PK__tbEscala__A6FB417FE5BEBAC8");
        });

        modelBuilder.Entity<TbEscalaBristolPacienteConsulta>(entity =>
        {
            entity.HasKey(e => e.IdEscalaBristolPacienteConsulta).HasName("PK__tbEscala__44B7B8966A35F545");

            entity.HasOne(d => d.IdEscalaBristolNavigation).WithMany(p => p.TbEscalaBristolPacienteConsulta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbEscalaB__IdEsc__17F790F9");

            entity.HasOne(d => d.IdHoraPacienteProfissionalNavigation).WithMany(p => p.TbEscalaBristolPacienteConsulta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbEscalaB__IdHor__19DFD96B");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.TbEscalaBristolPacienteConsulta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbEscalaB__IdPac__18EBB532");
        });

        modelBuilder.Entity<TbEstado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__tbEstado__FBB0EDC188F0ED55");

            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.TbEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbEstado__IdPais__398D8EEE");
        });

        modelBuilder.Entity<TbExame>(entity =>
        {
            entity.HasKey(e => e.IdExame).HasName("PK__tbExame__0C2F360F255CCA14");
        });

        modelBuilder.Entity<TbExameFisico>(entity =>
        {
            entity.HasKey(e => e.IdExameFisico).HasName("PK__tbExameF__C8897518CD27158B");

            entity.HasOne(d => d.IdHoraPacienteProfissionalNavigation).WithMany(p => p.TbExameFisico).HasConstraintName("FK__tbExameFi__IdHor__7A672E12");
        });

        modelBuilder.Entity<TbExameXPacientes>(entity =>
        {
            entity.HasKey(e => e.IdExameXPaciente).HasName("PK__tbExame___8F261547D75B06D7");

            entity.HasOne(d => d.IdExameNavigation).WithMany(p => p.TbExameXPacientes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbExame_X__IdExa__08B54D69");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.TbExameXPacientes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbExame_X__IdPac__09A971A2");
        });

        modelBuilder.Entity<TbGrupoPatologico>(entity =>
        {
            entity.HasKey(e => e.IdGrupoPatologico).HasName("PK__tbGrupoP__A7D4244D8473F997");
        });

        modelBuilder.Entity<TbGrupoPatologicoXPatologia>(entity =>
        {
            entity.HasKey(e => e.IdPatologiaXGrupoPatologico).HasName("PK__tbGrupoP__2BA30300C74D500A");

            entity.HasOne(d => d.IdGrupoPatologicoNavigation).WithMany(p => p.TbGrupoPatologicoXPatologia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbGrupoPa__IdGru__5070F446");

            entity.HasOne(d => d.IdPatologiaNavigation).WithMany(p => p.TbGrupoPatologicoXPatologia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbGrupoPa__IdPat__5165187F");
        });

        modelBuilder.Entity<TbGruposReceitasDespesas>(entity =>
        {
            entity.HasKey(e => e.IdReceitaDespesa).HasName("PK__tbGrupos__6D0EB5405BF2A8A2");
        });

        modelBuilder.Entity<TbHistoriaPatologica>(entity =>
        {
            entity.HasKey(e => e.IdHistoriaPatologica).HasName("PK__tbHistor__86BDE8CF1E996F4D");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.TbHistoriaPatologica)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbHistori__IdPac__76969D2E");

            entity.HasOne(d => d.IdPatologiaNavigation).WithMany(p => p.TbHistoriaPatologica).HasConstraintName("FK__tbHistori__IdPat__778AC167");
        });

        modelBuilder.Entity<TbHistoricoAlimentarNutricional>(entity =>
        {
            entity.HasKey(e => e.IdHistAlimentarNutricional).HasName("PK__tbHistor__43605910B8D41005");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.TbHistoricoAlimentarNutricional)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbHistori__IdPac__00200768");
        });

        modelBuilder.Entity<TbHistoricoDoencaAtual>(entity =>
        {
            entity.HasKey(e => e.IdHda).HasName("PK__tbHistor__0B5E79E21AD7740A");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.TbHistoricoDoencaAtual)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbHistori__IdPac__72C60C4A");

            entity.HasOne(d => d.IdPatologiaNavigation).WithMany(p => p.TbHistoricoDoencaAtual).HasConstraintName("FK__tbHistori__IdPat__73BA3083");
        });

        modelBuilder.Entity<TbHistoricoSocialAlimentar>(entity =>
        {
            entity.HasKey(e => e.IdHistSocialAlimentar).HasName("PK__tbHistor__7735810FEB315D96");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.TbHistoricoSocialAlimentar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbHistori__IdPac__7D439ABD");
        });

        modelBuilder.Entity<TbHoraPacienteProfissional>(entity =>
        {
            entity.HasKey(e => e.IdHoraPacienteProfissional).HasName("PK__tbHoraPa__A67184260E014819");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.TbHoraPacienteProfissional)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbHoraPac__IdPac__6EF57B66");

            entity.HasOne(d => d.IdProfissionalNavigation).WithMany(p => p.TbHoraPacienteProfissional)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbHoraPac__IdPro__6FE99F9F");
        });

        modelBuilder.Entity<TbLancarReceitasDespesas>(entity =>
        {
            entity.HasKey(e => e.IdLancamento).HasName("PK__tbLancar__2E531549C4400B28");

            entity.HasOne(d => d.IdReceitaDespesaNavigation).WithMany(p => p.TbLancarReceitasDespesas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbLancarR__IdRec__1EA48E88");
        });

        modelBuilder.Entity<TbMedicamento>(entity =>
        {
            entity.HasKey(e => e.IdMedicamento).HasName("PK__tbMedica__AC96376E90E9D5E4");

            entity.HasOne(d => d.IdCategoriaMedicamentoNavigation).WithMany(p => p.TbMedicamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbMedicam__IdCat__5812160E");
        });

        modelBuilder.Entity<TbMedicoPaciente>(entity =>
        {
            entity.HasKey(e => e.IdMedicoPaciente).HasName("PK__tbMedico__1F0BE44C0A2AD1D5");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.TbMedicoPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbMedico___IdPac__6B24EA82");

            entity.HasOne(d => d.IdProfissionalNavigation).WithMany(p => p.TbMedicoPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbMedico___IdPro__6C190EBB");
        });

        modelBuilder.Entity<TbPaciente>(entity =>
        {
            entity.HasKey(e => e.IdPaciente).HasName("PK__tbPacien__C93DB49B87CE1DFE");

            entity.Property(e => e.Sexo).IsFixedLength();

            entity.HasOne(d => d.IdCidadeNavigation).WithMany(p => p.TbPaciente).HasConstraintName("FK__tbPacient__IdCid__68487DD7");
        });

        modelBuilder.Entity<TbPais>(entity =>
        {
            entity.HasKey(e => e.IdPais).HasName("PK__tbPais__FC850A7B0E4B8FB8");
        });

        modelBuilder.Entity<TbPatologia>(entity =>
        {
            entity.HasKey(e => e.IdPatologia).HasName("PK__tbPatolo__6D573A32CB2159DD");
        });

        modelBuilder.Entity<TbPergunta>(entity =>
        {
            entity.HasKey(e => e.IdPergunta).HasName("PK__tbPergun__6DC6D9A7D4EDA3D1");

            entity.HasOne(d => d.IdProfissionalNavigation).WithMany(p => p.TbPergunta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbPergunt__IdPro__0C85DE4D");
        });

        modelBuilder.Entity<TbPlano>(entity =>
        {
            entity.HasKey(e => e.IdPlano).HasName("PK__tbPlano__4C970C540297154D");
        });

        modelBuilder.Entity<TbProfissional>(entity =>
        {
            entity.HasKey(e => e.IdProfissional).HasName("PK__tbProfis__B9503FBCD9D3008C");

            entity.HasOne(d => d.IdCidadeNavigation).WithMany(p => p.TbProfissional)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbProfiss__IdCid__49C3F6B7");

            entity.HasOne(d => d.IdContratoNavigation).WithMany(p => p.TbProfissional)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbProfiss__IdCon__47DBAE45");

            entity.HasOne(d => d.IdTipoAcessoNavigation).WithMany(p => p.TbProfissional).HasConstraintName("FK__tbProfiss__IdTip__48CFD27E");
        });

        modelBuilder.Entity<TbRastreamentoMetabolico>(entity =>
        {
            entity.HasKey(e => e.IdRastreamentoMetabolico).HasName("PK__tbRastre__E5AA062354B3AF6E");

            entity.HasOne(d => d.IdHoraPacienteProfissionalNavigation).WithMany(p => p.TbRastreamentoMetabolico)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbRastrea__IdHor__1332DBDC");

            entity.HasOne(d => d.IdRastreamentoRespostaNavigation).WithMany(p => p.TbRastreamentoMetabolico)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbRastrea__IdRas__123EB7A3");
        });

        modelBuilder.Entity<TbRastreamentoResposta>(entity =>
        {
            entity.HasKey(e => e.IdRastreamentoResposta).HasName("PK__tbRastre__ABA70EB6ADFF52D5");

            entity.HasOne(d => d.IdPerguntaNavigation).WithMany(p => p.TbRastreamentoResposta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbRastrea__IdPer__0F624AF8");
        });

        modelBuilder.Entity<TbReceitaAlimentarPadrao>(entity =>
        {
            entity.HasKey(e => e.IdReceitaAlimentarPadrao).HasName("PK__tbReceit__63863561B967240D");

            entity.HasOne(d => d.IdProfissionalNavigation).WithMany(p => p.TbReceitaAlimentarPadrao).HasConstraintName("FK__tbReceita__IdPro__5EBF139D");
        });

        modelBuilder.Entity<TbReceitaAlimentarPadraoXAlimento>(entity =>
        {
            entity.HasKey(e => e.IdReceitaAlimentarPadraoXAlimentoXQuantidadeAlimento).HasName("PK__tbReceit__2843B293137E4EBF");

            entity.HasOne(d => d.IdAlimentoNavigation).WithMany(p => p.TbReceitaAlimentarPadraoXAlimento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbReceita__IdAli__656C112C");

            entity.HasOne(d => d.IdReceitaAlimentarPadraoNavigation).WithMany(p => p.TbReceitaAlimentarPadraoXAlimento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbReceita__IdRec__6477ECF3");
        });

        modelBuilder.Entity<TbReceitaMedicaPadrao>(entity =>
        {
            entity.HasKey(e => e.IdReceitaMedicaPadrao).HasName("PK__tbReceit__E6DF7D77D9F2691C");

            entity.HasOne(d => d.IdProfissionalNavigation).WithMany(p => p.TbReceitaMedicaPadrao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbReceita__IdPro__619B8048");
        });

        modelBuilder.Entity<TbSubstancia>(entity =>
        {
            entity.HasKey(e => e.IdSubstancia).HasName("PK__tbSubsta__13276D321A469721");
        });

        modelBuilder.Entity<TbSuplemento>(entity =>
        {
            entity.HasKey(e => e.IdSuplemento).HasName("PK__tbSuplem__EC3A6B160D0E538F");
        });

        modelBuilder.Entity<TbTipoAcesso>(entity =>
        {
            entity.HasKey(e => e.IdTipoAcesso).HasName("PK__tbTipoAc__FFC17AE5BEE97553");
        });

        modelBuilder.Entity<TbTipoProfissional>(entity =>
        {
            entity.HasKey(e => e.IdTipoProfissional).HasName("PK__tbTipoPr__358F03BEAA1B9B6E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}