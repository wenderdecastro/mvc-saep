using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using simuladosaep.Models;

namespace simuladosaep.Contexts;

public partial class DbDevContext : DbContext
{
    public DbDevContext()
    {
    }

    public DbDevContext(DbContextOptions<DbDevContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Atividade> Atividades { get; set; }

    public virtual DbSet<Professor> Professors { get; set; }

    public virtual DbSet<Turma> Turmas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=NOTE06-S21\\SQLEXPRESS;Initial Catalog=db_dev;user ID=sa;pwd=Senai@134;Trusted_Connection=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Atividade>(entity =>
        {
            entity.HasKey(e => e.AtividadeId).HasName("PK__Atividad__742A5D1451F04D54");

            entity.ToTable("Atividade");

            entity.Property(e => e.AtividadeId).HasColumnName("AtividadeID");
            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TurmaId).HasColumnName("TurmaID");

            entity.HasOne(d => d.Turma).WithMany(p => p.Atividades)
                .HasForeignKey(d => d.TurmaId)
                .HasConstraintName("FK__Atividade__Turma__4D94879B");
        });

        modelBuilder.Entity<Professor>(entity =>
        {
            entity.HasKey(e => e.ProfessorId).HasName("PK__Professo__90035969B755F239");

            entity.ToTable("Professor");

            entity.Property(e => e.ProfessorId).HasColumnName("ProfessorID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Senha)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Turma>(entity =>
        {
            entity.HasKey(e => e.TurmaId).HasName("PK__Turma__BABB9364762717B3");

            entity.ToTable("Turma");

            entity.Property(e => e.TurmaId).HasColumnName("TurmaID");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ProfessorId).HasColumnName("ProfessorID");

            entity.HasOne(d => d.Professor).WithMany(p => p.Turmas)
                .HasForeignKey(d => d.ProfessorId)
                .HasConstraintName("FK__Turma__Professor__4E88ABD4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
